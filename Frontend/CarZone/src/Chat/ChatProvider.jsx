import React, { useEffect, useState, useRef } from 'react';
import * as signalR from "@microsoft/signalr";
import { ChatContext } from './ChatContext';
import axios from 'axios';
import toast from 'react-hot-toast'
const ChatProvider = ({ children }) => {
    const user = JSON.parse(localStorage.getItem('user')) || {};
    const [allMessages, setAllMessages] = useState([]);
    const [connection, setConnection] = useState(null);
    const connectionRef = useRef(null);

    // Fetch svih poruka
    useEffect(() => {
        if (!user) return;
        fetchMessages();
    }, [user.email]);

    const fetchMessages = async () => {
        try {
            const response = await axios.get(`http://localhost:5047/message`);
            setAllMessages(response.data.filter(m => m.sender.email === user.email || m.receiver.email === user.email));
        } catch (error) {
            console.log(error);
        }
    };
    // SignalR konekcija
    useEffect(() => {
        if (!user) return;
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
        newConnection.on("ReceiveMessage", (message) => {
            fetchMessages();
            var receiverUser = null;
            axios.get(`http://localhost:5047/user/${message.receiverId}`).then((response) => {
                receiverUser = response.data
            }).catch((error) => {
                console.log(error)
            })
            toast.custom((t) => (
                <div style={{
                    backgroundColor: 'rgba(255, 255, 255, 0.95)',
                    backdropFilter: 'blur(8px)',
                    width: '280px',
                    padding: '12px 16px',
                    borderRadius: '16px',
                    border: '1px solid rgba(0, 0, 0, 0.05)',
                    boxShadow: '0 10px 25px -5px rgba(0, 0, 0, 0.1), 0 8px 10px -6px rgba(0, 0, 0, 0.1)',

                    display: 'flex',
                    flexDirection: 'column',
                    gap: '4px',
                    fontFamily: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif',

                    opacity: t.visible ? 1 : 0,
                    transition: 'all 0.3s ease',
                }}>
                    <div style={{
                        fontSize: '13px',
                        fontWeight: '700',
                        color: '#111827',
                        letterSpacing: '-0.01em'
                    }}>
                        {receiverUser?.firstName} {receiverUser?.lastName}
                    </div>

                    <div style={{
                        fontSize: '13px',
                        lineHeight: '1.4',
                        color: '#4b5563',
                        overflow: 'hidden',
                        display: '-webkit-box',
                        WebkitLineClamp: '2',
                        WebkitBoxOrient: 'vertical',
                    }}>
                        {message?.messageText}
                    </div>

                    <button
                        onClick={() => toast.dismiss(t.id)}
                        style={{
                            alignSelf: 'flex-end',
                            marginTop: '4px',
                            padding: '4px 8px',
                            backgroundColor: 'transparent',
                            border: 'none',
                            color: '#6366f1',
                            fontSize: '11px',
                            fontWeight: '600',
                            textTransform: 'uppercase',
                            letterSpacing: '0.05em',
                            cursor: 'pointer',
                            borderRadius: '6px',
                            transition: 'background 0.2s'
                        }}
                        onMouseOver={(e) => e.target.style.backgroundColor = '#f3f4f6'}
                        onMouseOut={(e) => e.target.style.backgroundColor = 'transparent'}
                    >
                        Close
                    </button>
                </div>
            ));
        });

        return () => {
            if (connectionRef.current) {
                connectionRef.current.stop();
                connectionRef.current = null;
                setConnection(null);
            }
        };
    }, [user?.token]);

    // Slanje poruke
    const sendMessage = async (receiverEmail, text, offer = null) => {
        if (!connection || connection.state !== signalR.HubConnectionState.Connected) {
            console.warn("Konekcija nije aktivna. Trenutno stanje:", connection?.state);
            return;
        }

        let receiverUser = null;
        try {
            const response = await axios.get(`http://localhost:5047/user/email/${receiverEmail}`);
            receiverUser = response.data;
        } catch (error) {
            console.log(error);
            return;
        }

        const payload = {
            messageText: text,
            senderId: user.userId,
            receiverId: receiverUser.userId,
            listingId: offer?.listingId ?? null
        };

        try {
            // Invoke SignalR hub
            await connection.invoke("SendMessage", receiverEmail, payload);

            // Opcionalno: dodaj odmah lokalno da se vidi instant
            fetchMessages()
        } catch (err) {
            console.error("Greška pri slanju poruke: ", err);
        }
    };

    return (
        <ChatContext.Provider value={{ connection, allMessages, sendMessage, setAllMessages }}>
            {children}
        </ChatContext.Provider>
    );
};

export default ChatProvider;