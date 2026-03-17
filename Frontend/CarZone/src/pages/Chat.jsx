import React, { useContext, useEffect, useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import "../styles/Chat.css";
import LogedNavbar from "../components/LogedNavbar";
import Footer from "../components/Footer";
import { ChatContext } from "../Chat/ChatContext";

const Chat = () => {
  const { allMessages, sendMessage } = useContext(ChatContext);
  const user = JSON.parse(localStorage.getItem("user"));

  const location = useLocation();
  const navigate = useNavigate();

  const [message, setMessage] = useState("");
  const [receiverEmail, setReceiverEmail] = useState("");
  const [contextOffer, setContextOffer] = useState(null);
  const [chatUsers, setChatUsers] = useState([]);

  const messagesEndRef = useRef(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  useEffect(() => {
    scrollToBottom();
  }, [allMessages]);

  const getLastOfferFromMessages = (email) => {
    const lastMsg = [...allMessages].reverse().find(
      (m) =>
        ((m.sender.email === email && m.receiver.email === user.email) ||
          (m.sender.email === user.email &&
            m.receiver.email === email)) &&
        m.listing
    );

    return lastMsg?.listing || null;
  };

  useEffect(() => {
    if (location.state?.receiver) {
      setReceiverEmail(location.state.receiver);
    }

    if (location.state?.offer) {
      setContextOffer(location.state.offer);
    }
  }, [location.state]);

  useEffect(() => {
    if (!receiverEmail) return;

    if (!contextOffer) {
      const offer = getLastOfferFromMessages(receiverEmail);
      setContextOffer(offer);
    }
  }, [receiverEmail, allMessages]);

  useEffect(() => {
    const users = allMessages
      .filter(
        (m) =>
          m.sender.email === user.email || m.receiver.email === user.email
      )
      .map((m) =>
        m.sender.email === user.email
          ? m.receiver.email
          : m.sender.email
      );

    setChatUsers([...new Set(users)]);
  }, [allMessages]);

  const handleSendMessage = () => {
    if (!receiverEmail || !message) return;

    sendMessage(receiverEmail, message, contextOffer);
    setMessage("");
  };

  const handleUserClick = (email) => {
    setReceiverEmail(email);

    const offer = getLastOfferFromMessages(email);
    setContextOffer(offer);
  };

  return (
    <div className="chat-wrapper">
      <LogedNavbar />

      <div className="chat-body">
        <aside className="chat-users">
          <h4>Chats</h4>
          <ul>
            {chatUsers.map((email) => (
              <li
                key={email}
                className={
                  receiverEmail === email
                    ? "chaters active-user"
                    : "chaters"
                }
                onClick={() => handleUserClick(email)}
              >
                {email}
              </li>
            ))}
          </ul>
        </aside>

        <section className="chat-window">
          {contextOffer && (
            <div
              className="chat-context-bar"
              onClick={() =>
                navigate("/offer-details", {
                  state: { offer: contextOffer },
                })
              }
            >
              <img
                src={`http://localhost:5047/${contextOffer.images[0]?.imageUrl}`}
                alt="car"
                style={{
                  width: "50px",
                  height: "40px",
                  objectFit: "cover",
                  borderRadius: "4px",
                }}
              />

              <div className="context-info">
                <span>
                  {contextOffer?.model?.brandName}{" "}
                  {contextOffer?.model?.modelName}
                </span>
                <small>{contextOffer?.price} €</small>
              </div>

              <button
                className="close-context"
                onClick={(e) => {
                  e.stopPropagation();
                  setContextOffer(null);
                }}
              >
                ×
              </button>
            </div>
          )}

          <div className="messages">
            {allMessages
              .filter(
                (m) =>
                  (m.sender.email === user.email &&
                    m.receiver.email === receiverEmail) ||
                  (m.sender.email === receiverEmail &&
                    m.receiver.email === user.email)
              )
              .map((m, index) => (
                <div
                  key={index}
                  className={
                    m.sender.email === user.email
                      ? "message-sent-div"
                      : "message-received-div"
                  }
                >
                  {m.messageText}
                </div>
              ))}

            <div ref={messagesEndRef} />
          </div>

          <div className="chat-input">
            <input
              type="email"
              placeholder="Receiver email"
              value={receiverEmail}
              onChange={(e) => setReceiverEmail(e.target.value)}
            />

            <input
              type="text"
              placeholder="Type a message..."
              value={message}
              onChange={(e) => setMessage(e.target.value)}
              onKeyDown={(e) =>
                e.key === "Enter" && handleSendMessage()
              }
            />

            <button onClick={handleSendMessage}>Send</button>
          </div>
        </section>
      </div>

      <Footer />
    </div>
  );
};

export default Chat;