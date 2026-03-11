import React, { useEffect, useState, useRef } from 'react';
import * as signalR from "@microsoft/signalr";
import { ChatContext } from './ChatContext';

const ChatProvider = ({ children }) => {
    const [allMessages, setAllMessages] = useState(() => {
        const saved = localStorage.getItem('chat_messages');
        return saved ? JSON.parse(saved) : [];
    });

    const [chatUsers, setChatUsers] = useState(() => {
        const saved = localStorage.getItem('chat_users');
        return saved ? JSON.parse(saved) : [];
    });

    const [connection, setConnection] = useState(null);
    const connectionRef = useRef(null);
    const user = JSON.parse(localStorage.getItem('user'));

    // LocalStorage sync
    useEffect(() => {
        localStorage.setItem('chat_messages', JSON.stringify(allMessages));
        localStorage.setItem('chat_users', JSON.stringify(chatUsers));
    }, [allMessages, chatUsers]);

    // SignalR konekcija
    useEffect(() => {
        if (!user?.token || connectionRef.current) return;

        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5047/chat", {
                accessTokenFactory: () => user.token,
                transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        connectionRef.current = newConnection;

        newConnection.start()
            .then(() => {
                console.log("SignalR Connected!");
                setConnection(newConnection);
            })
            .catch(err => console.error("SignalR Connection Error: ", err));

        // Primanje poruka
        newConnection.on("ReceiveMessage", (senderEmail, messageText) => {
            let parsedText = messageText;
            let receivedOffer = null;

            try {
                const data = JSON.parse(messageText);
                parsedText = data.text || messageText;
                receivedOffer = data.offer || null;
            } catch (e) {
                parsedText = messageText;
            }

            const receivedMsg = {
                senderEmail,
                receiverEmail: user.email,
                message: parsedText,
                associatedOffer: receivedOffer,
                timestamp: new Date().getTime()
            };

            setAllMessages(prev => [...prev, receivedMsg]);
            setChatUsers(prev => prev.includes(senderEmail) ? prev : [senderEmail, ...prev]);
        });

        // Cleanup
        return () => {
            if (connectionRef.current) {
                connectionRef.current.stop();
                connectionRef.current = null;
                setConnection(null);
            }
        };
    }, []);

    // Slanje poruke sa offer
    const sendMessage = async (receiverEmail, text, offer = null) => {
        if (connection && connection.state === signalR.HubConnectionState.Connected) {
            const payload = JSON.stringify({ text, offer });

            const messageData = {
                senderEmail: user.email,
                receiverEmail,
                message: text,
                associatedOffer: offer,
                timestamp: new Date().getTime()
            };

            try {
                await connection.invoke("SendMessage", receiverEmail, payload);

                setAllMessages(prev => [...prev, messageData]);
                setChatUsers(prev => prev.includes(receiverEmail) ? prev : [...prev, receiverEmail]);
            } catch (err) {
                console.error("Greška pri slanju poruke: ", err);
            }
        } else {
            console.warn("Konekcija nije aktivna. Trenutno stanje:", connection?.state);
        }
    };

    return (
        <ChatContext.Provider value={{ connection, allMessages, chatUsers, sendMessage, setAllMessages, setChatUsers }}>
            {children}
        </ChatContext.Provider>
    );
};

export default ChatProvider;