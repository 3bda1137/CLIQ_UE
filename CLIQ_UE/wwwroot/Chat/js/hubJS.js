
var chatHub = new signalR.HubConnectionBuilder().withUrl("/ChatIndividual").build();
chatHub.start().then(function () {
    console.log("************************************");
    console.log("start Connection with ChatIndividual");
    console.log("************************************");
});

function sendMessage(otherUser) {//otherUser --> The user to whom the message will be sent
    console.log("----------Start sendMessage --------------")
    var inputMessage = document.getElementById("messageContant");
    let message = inputMessage.value;
    inputMessage.value = "";
    const currentId = document.querySelector('#currentId').textContent;//my ID
    console.log("My ID :", currentId);
    chatHub.invoke("SaveMessage", message, currentId, otherUser);
    console.log("invoke complete");

    console.log("----------end sendMessage --------------")
}

chatHub.on("desplayMessageForCaller", function (messageresevd) {

    console.log("----------start desplayMessageForCaller------------")
    let msg = "";
    msg += `
        <li class="Conversation_Item me">
            <div class="Conversation_Item_Side">
                <img src="/chat/images/test.jpg" class="Conversation_Item_Image">
            </div>
            <div class="Conversation_Item_Content">
                <div class="Conversation_Item_Wrapper">
                    <div class="Conversation_Item_Box">
                        <div class="Conversation_Item_text">
                            <p>
                                ${messageresevd.messageContant}
                            </p>
                            <span class="Conversation_Item_Time"> ${messageresevd.time}</span>
                        </div>
                        <div class="Conversation_Item_dropdown">
                            <button type="button" class="Conversation_Item_dropdown_toggle">
                                <i class="ri-more-2-line"></i>
                            </button>
                            <ul class="Conversation_Item_dropdown_List">
                                <li><a href="#"><i class="ri-share-forward-line"></i>Forward</a></li>
                                <li><a href="#"><i class="ri-delete-bin-line"></i>Delete</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        `
    document.getElementById("massegeList").innerHTML += msg;

    console.log("----------end desplayMessageForCaller------------")

});
chatHub.on("displayMessage", function (messageresevd) {
    console.log("----------start displayMessage------------")
    const currentId = document.querySelector('#currentId').textContent;//my ID
    const otherUserid = document.querySelector('#otherUserid').textContent;
    console.log("otherUserid: ", otherUserid)
    console.log("currentId: ", currentId)
    console.log("Message:", messageresevd)
    let msg = "";
    if (otherUserid == messageresevd.senderId && currentId == messageresevd.receiverId) {
        msg += ` 
            <li class="Conversation_Item">
                <div class="Conversation_Item_Side">
                    <img src="/chat/images/test.jpg" class="Conversation_Item_Image">
                </div>
                <div class="Conversation_Item_Content">
                    <div class="Conversation_Item_Wrapper">
                        <div class="Conversation_Item_Box">
                            <div class="Conversation_Item_text">
                                <p>
                                    ${messageresevd.messageContant}
                                </p>
                                <span class="Conversation_Item_Time"> ${messageresevd.time}</span>
                            </div>
                            <div class="Conversation_Item_dropdown">
                                <button type="button" class="Conversation_Item_dropdown_toggle">
                                    <i class="ri-more-2-line"></i>
                                </button>
                                <ul class="Conversation_Item_dropdown_List">
                                    <li><a href="#"><i class="ri-share-forward-line"></i>Forward</a></li>
                                    <li><a href="#"><i class="ri-delete-bin-line"></i>Delete</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
            </li>
       `

        document.getElementById("massegeList").innerHTML += msg;
    }
    console.log("----------end displayMessage------------")

});

var OtherUSerIDToChack = "";
chatHub.on("upDateListUsers", function (user) {
    console.log("--------start upDateListUsers----------");
    console.log(user);
    const liToRemove = document.getElementById(user.userId);
    console.log(liToRemove);
    const ul = document.querySelector('#myFollowingUsers');
    OtherUSerIDToChack = user.lastMessage.reseverID;
    if (liToRemove && ul) {
        ul.removeChild(liToRemove);
        const newLi = document.createElement('li');
        newLi.id = user.userId;
        newLi.innerHTML = `
            <div onclick="showChat('${user.userId}','${user.userName}','${user.imageUrl}')" data-conversation="#Conversation1">
                <img class="Content_Message_image" src="${user.imageUrl}">
                <span class="Content_Message_info">
                    <span class="Content_Message_Name">${user.userName}</span>
                    ${user.lastMessage ? `<span class="Content_Message_Text">${user.lastMessage.message}</span>` : ''}
                </span>
                <span class="Content_Message_More">
                    
                    <span class="Content_Message_Time">${user.formatedTime}</span>
                </span>
            </div>
        `;
        ul.insertBefore(newLi, ul.firstChild);
    }
    console.log("--------end upDateListUsers----------")

});

chatHub.on("upDateListUsersInOTherUser", function (user, otherUserId) {
    console.log("--------start upDateListUsersInOTherUser----------")

    console.log(user)

    const liToRemove = document.getElementById(user.userId);
    console.log(liToRemove);
    const ul = document.querySelector('#myFollowingUsers');

    if (liToRemove && ul) {
        const currentId = document.querySelector('#currentId').textContent;
        console.log("currentId: ", currentId);
        console.log("otherUserId: ", otherUserId);

        if (currentId == otherUserId) {

            ul.removeChild(liToRemove);
            const newLi = document.createElement('li');
            newLi.id = user.userId;
            newLi.innerHTML = `
                <div onclick="showChat('${user.userId}','${user.userName}','${user.imageUrl}')" data-conversation="#Conversation1">
                    <img class="Content_Message_image" src="${user.imageUrl}">
                    <span class="Content_Message_info">
                        <span class="Content_Message_Name">${user.userName}</span>
                        ${user.lastMessage ? `<span class="Content_Message_Text">${user.lastMessage.message}</span>` : ''}
                    </span>
                    <span class="Content_Message_More">
                        <span class="Content_Message_Time">${user.formatedTime}</span>
                    </span>
                </div>
            `;

            ul.insertBefore(newLi, ul.firstChild);
        }
    }
    console.log("--------end upDateListUsersInOTherUser----------")

});
