import React, { useContext, useEffect, useRef, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import '../styles/Chat.css';
import LogedNavbar from '../components/LogedNavbar';
import Footer from '../components/Footer';
import { ChatContext } from '../Chat/ChatContext';

const Chat = () => {
  const { allMessages, chatUsers, sendMessage } = useContext(ChatContext);
  const user = JSON.parse(localStorage.getItem('user'));
  const location = useLocation();
  const navigate = useNavigate();

  const [message, setMessage] = useState('');
  const [receiverEmail, setReceiverEmail] = useState('');
  const [contextOffer, setContextOffer] = useState(null);
  const messagesEndRef = useRef(null);

  // Scroll na poslednju poruku
  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  useEffect(() => {
    scrollToBottom();
  }, [allMessages, receiverEmail, contextOffer]);

  // Postavljanje receiverEmail i contextOffer kad dolazi sa OfferDetails
  useEffect(() => {
    if (location.state?.receiver) setReceiverEmail(location.state.receiver);
    if (location.state?.offer) setContextOffer(location.state.offer);
  }, [location.state?.receiver, location.state?.offer]);

  // Automatsko postavljanje contextOffer kad se menja receiver ili stigne nova poruka
  useEffect(() => {
    if (!receiverEmail) return;

    // Ako je offer već inicijalno postavljen iz location.state, ne prepisuj ga
    if (location.state?.offer && receiverEmail === location.state.receiver) return;

    const lastMsgWithOffer = [...allMessages]
      .reverse()
      .find(
        m =>
          ((m.senderEmail === receiverEmail && m.receiverEmail === user.email) ||
           (m.senderEmail === user.email && m.receiverEmail === receiverEmail)) &&
          m.associatedOffer
      );

    setContextOffer(lastMsgWithOffer?.associatedOffer || null);
  }, [allMessages, receiverEmail, location.state, user.email]);

  const handleSendMessage = () => {
    if (!receiverEmail || !message) return;

    sendMessage(receiverEmail, message, contextOffer);
    setMessage('');
  };

  const handleUserClick = (email) => {
    setReceiverEmail(email);

    // Pronađi poslednji offer za kliknutog korisnika
    const lastMsgWithOffer = [...allMessages]
      .reverse()
      .find(
        m =>
          ((m.senderEmail === email && m.receiverEmail === user.email) ||
           (m.senderEmail === user.email && m.receiverEmail === email)) &&
          m.associatedOffer
      );

    setContextOffer(lastMsgWithOffer?.associatedOffer || null);
  };

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
                className={receiverEmail === email ? 'chaters active-user' : 'chaters'}
                onClick={() => handleUserClick(email)}
              >
                {email}
              </li>
            ))}
          </ul>
        </aside>

        <section className="chat-window">

          {/* Context offer bar */}
          {contextOffer && (
            <div
              className="chat-context-bar"
              onClick={() => navigate('/offer-details', { state: { offer: contextOffer } })}
            >
              <img
                src={`http://localhost:5047/${contextOffer.images[0]?.imageUrl}`}
                alt="car"
                style={{ width: '50px', height: '40px', objectFit: 'cover', borderRadius: '4px' }}
              />
              <div className="context-info">
                <span>{contextOffer.model.brandName} {contextOffer.model.modelName}</span>
                <small>{contextOffer.price} €</small>
              </div>
              <button
                className="close-context"
                onClick={(e) => { e.stopPropagation(); setContextOffer(null); }}
              >
                ×
              </button>
            </div>
          )}

          <div className="messages">
            {allMessages
              .filter(m =>
                (m.senderEmail === user.email && m.receiverEmail === receiverEmail) ||
                (m.senderEmail === receiverEmail && m.receiverEmail === user.email)
              )
              .map((m, index) => (
                <div
                  key={index}
                  className={m.senderEmail === user.email ? "message-sent-div" : "message-received-div"}
                >
                  {m.message}
                </div>
              ))}
            <div ref={messagesEndRef} />
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

            <button onClick={handleSendMessage}>Send</button>
          </div>
        </section>
      </div>

      <Footer />
    </div>
  );
};

export default Chat;