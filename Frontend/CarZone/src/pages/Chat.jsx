import React, { useRef, useState } from 'react'
import { useEffect } from 'react'
import * as signalR from "@microsoft/signalr";
import { useLocation } from 'react-router-dom';
import '../styles/Chat.css'
import LogedNavbar from '../components/LogedNavbar'
import Footer from '../components/Footer'

const Chat = () => {
  const connectionRef = useRef(null)
  const user = JSON.parse(localStorage.getItem('user'))
  const [message, setMessage] = useState('')
  const [receiverEmail, setReceiverEmail] = useState('')

  const [allMessages, setAllMessages] = useState(() => {
    const savedMessages = localStorage.getItem('chat_messages');
    return savedMessages ? JSON.parse(savedMessages) : []
  })

  const [chatUsers, setChatUsers] = useState(() => {
    const savedUsers = localStorage.getItem('chat_users');
    return savedUsers ? JSON.parse(savedUsers) : [];
  })



  useEffect(() => {
    localStorage.setItem('chat_messages', JSON.stringify(allMessages));
    localStorage.setItem('chat_users', JSON.stringify(chatUsers));
  }, [allMessages, chatUsers]);

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5047/chat", {
      accessTokenFactory: () => user.token,
      transport: signalR.HttpTransportType.WebSockets
    }).withAutomaticReconnect()
      .build();

    connectionRef.current = connection;

    connection.start()
      .then(() => console.log("Connected to chat hub"))
      .catch(err => console.error("Connection failed: ", err));

    connection.on("ReceiveMessage", (senderEmail, messageText) => {
      setAllMessages(prev => [...prev, { senderEmail: senderEmail, receiverEmail: user.email, message: messageText }])
      setChatUsers(prev =>
        prev.includes(senderEmail) ? prev : [...prev, senderEmail]
      )
    })

    return () => {
      if (connectionRef.current) {
        connectionRef.current.stop()
          .then(() => console.log("Connection stopped"))
          .catch(err => console.log("Error stopping:", err));
      }
    }

  }, [])


  const handleSendMessage = () => {
    connectionRef.current.invoke("SendMessage", receiverEmail, message)

    setAllMessages(prev => [...prev, { senderEmail: user.email, receiverEmail: receiverEmail, message: message }]);
    setChatUsers(prev =>
      prev.includes(receiverEmail) ? prev : [...prev, receiverEmail]
    );
    setMessage("");
  }
  return (
    <div className="chat-wrapper">
      <LogedNavbar />

      <div className="chat-body">

        <aside className="chat-users">
          <h4>Chats</h4>
          <ul>
            {chatUsers.map(email => (
              <li
                key={email}
                className={'chaters'}
                onClick={() => setReceiverEmail(email)}
              >
                {email}
              </li>
            ))}
          </ul>
        </aside>

        <section className="chat-window">

          <div className="messages">
            {
              allMessages
                .filter(m =>
                  (m.senderEmail === user.email && m.receiverEmail === receiverEmail) ||
                  (m.senderEmail === receiverEmail && m.receiverEmail === user.email)
                )
                .map((m, index) => (
                  <div
                    key={index}
                    className={
                      m.senderEmail === user.email
                        ? "message-sent-div"
                        : "message-received-div"
                    }
                  >
                    {m.message}
                  </div>
                ))}
          </div>

          <div className="chat-input">
            <input
              type="email"
              placeholder="Receiver email"
              value={receiverEmail}
              onChange={e => setReceiverEmail(e.target.value)}
            />

            <input
              type="text"
              placeholder="Type a message..."
              value={message}
              onChange={e => setMessage(e.target.value)}
              onKeyDown={e => e.key === "Enter" && handleSendMessage()}
            />

            <button onClick={handleSendMessage}>
              Send
            </button>
          </div>

        </section>
      </div>
      <Footer />
    </div>
  );
}

export default Chat