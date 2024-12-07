// chat.js

function scrollToBottom() {
    const chatContainer = document.querySelector('.chat-container');
    chatContainer.scrollTop = chatContainer.scrollHeight;
}

window.addEventListener('load', scrollToBottom);

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    console.log("Connected to the SignalR hub!");
}).catch(function (err) {
    console.error(err.toString());
});

connection.on("ReceiveMessage", function (nickname, profilePicture, message, timestamp, isSentByCurrentUser) {
    const messageContainer = document.createElement("div");
    messageContainer.className = isSentByCurrentUser ? "message-container sent" : "message-container received";

    const messageText = document.createElement("div");
    messageText.className = "message-text";

    const profileImg = document.createElement("img");
    profileImg.src = profilePicture;
    profileImg.style.width = "32px";
    profileImg.style.borderRadius = "50%";
    profileImg.style.marginRight = "6px";

    const textContent = document.createElement("span");
    textContent.textContent = `${nickname}: ${message}`;

    messageText.appendChild(profileImg);
    messageText.appendChild(textContent);

    const messageTimestamp = document.createElement("div");
    messageTimestamp.className = "message-timestamp";
    messageTimestamp.textContent = timestamp;

    messageContainer.appendChild(messageText);
    messageContainer.appendChild(messageTimestamp);
    document.getElementById("messagesList").appendChild(messageContainer);

    scrollToBottom();
});

function sendMessage(userId, receiverId, orderId) {
    const message = document.getElementById("messageInput").value;

    if (message.trim() !== "") {
        connection.invoke("SendMessage", userId, message, orderId, receiverId).catch(function (err) {
            console.error(err.toString());
        });
        document.getElementById("messageInput").value = ""; // Clear the input field
    }
}

document.getElementById("sendButton").addEventListener("click", function () {
    sendMessage(userId, receiverId, orderId);
});

document.getElementById("messageInput").addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        sendMessage(userId, receiverId, orderId);
    }
});
