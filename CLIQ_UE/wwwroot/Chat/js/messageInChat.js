
var otherUser = 0;
var followingImageUrl = "";
var followingName = "";
var lastSeen = "";
var tagOfLastSeen = "";
function showChat(otherUserId,otherUserName,otherUserImageUrl) {
    otherUser = otherUserId;
    followingImageUrl = otherUserImageUrl;
    followingName = otherUserName;
    console.log("----------Start showChat--------------")
    console.log("Other User ID :", otherUserId)

    //get last seen
    $.ajax({
        url: '/chat/GetLastSeen',
        type: 'GET',
        data: { otherUserId: otherUserId },
        success: function (data) {

            //console.log("Last seen : "+data);
            lastSeen = data;
        },
        error: function () {
            console.error('Error occurred while fetching search results.');
        }
    });
    //get messages 
    $.ajax({
        url: '/chat/GetMessages',
        type: 'GET',
        data: { otherUserId: otherUserId },
        success: function (data) {

            //console.log(data);
            displayChat(data);
        }, 
        error: function () {
            console.error('Error occurred while fetching search results.');
        }
    });

    console.log("----------end showChat--------------")
}
[
    {
        "id": 2,
        "senderId": "87680356-74e7-4c08-bfd1-f4c610b03260",
        "receiverId": "c105c759-f3fe-4a84-ad1f-6f176f1c20be",
        "createdAt": "2024-01-01T00:00:00",
        "time": "12",
        "messageContant": "hi  ahmed my name abdallah",
        "isDeleted": false,
        "isImage": false,
        "isReaded": false
    } 
]

function displayChat(results) {

    console.log("----------Start displayChat--------------")

    let msg = "";
    let chat = "";
    $.each(results, function (index, result) {
        //console.log("length -----= ", results.length)
        if (index + 1 < results.length) {
            var t1 = results[index].createdAt;
            var t2 = results[index + 1].createdAt;
            t1 = t1.split("T")[0];
            t2 = t2.split("T")[0];
        }
        if (index == 0) {
            msg +=
                `
                <div class="Conversation_Divider">
                    <span>
                        ${compareDate(t1)}
                    </span>
                </div>
                `
        }

        if (results[index].senderId == otherUser) {
            
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
                                    ${results[index].messageContant}
                                </p>
                                <span class="Conversation_Item_Time"> ${results[index].time}</span>
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
        }
        else {
            msg +=
                `
            <li class="Conversation_Item me">
                    <div class="Conversation_Item_Side">
                        <img src="/chat/images/test.jpg" class="Conversation_Item_Image">
                    </div>
                    <div class="Conversation_Item_Content">
                        <div class="Conversation_Item_Wrapper">
                            <div class="Conversation_Item_Box">
                                <div class="Conversation_Item_text">
                                    <p>
                                        ${results[index].messageContant}
                                    </p>
                                    <span class="Conversation_Item_Time">  ${results[index].time}</span>
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
        };
        if (t1 != t2) {
            msg +=
                `
                    <div class="Conversation_Divider">
                        <span>
                            ${compareDate(t2)}
                        </span>
                    </div>
                    `
        }
        
    });
    if (lastSeen == "Online") {
        tagOfLastSeen = `<div class="Conversation_User_Status online">${lastSeen}</div>`;
    } else {
        tagOfLastSeen = `<div class="Conversation_User_Status ">${lastSeen}</div>`;
    };
    chat = `
                <div class="Conversation_Top">
                    <button onclick="closeChat()" type="button" class="Conversation_Back"> <i class="ri-arrow-left-line"></i></button>
                    <div class="Conversation_User">
                        <img  src="${followingImageUrl}" class="Conversation_User_Image">
                        <div class="">
                            <div class="Conversation_User_Name">${followingName}</div>
                            ${tagOfLastSeen}
                            <span hidden id="otherUserid">${otherUser}</span>
                        </div>
                    </div>
                    <div class="Conversation_buttons">
                        <button type="button"><i class="ri-phone-fill"></i></button>
                        <button type="button"><i class="ri-vidicon-line"></i></button>
                        <button type="button"><i class="ri-information-line"></i></button>

                    </div>
                </div>
                <div class="Conversation_Main">
                    <ul id="massegeList" class="Conversation_Wrapper">
                        
                        ${msg}
                        
                    </ul>
                </div>
                <div class="Conversation_form">
                    <button type="button" class="Conversation_form_button" id="emoji-button">
                        <i class="ri-emotion-line"></i>
                    </button>
                    <div class="Conversation_form_group">
                        <textarea id="messageContant" class="Conversation_form_input" rows="1" placeholder="Message"> </textarea>
                        <button  type="button" class="Conversation_form_Record">
                            <i class="ri-mic-line"></i>
                        </button>
                    </div>
                    <button id="${otherUser}" onclick="sendMessage('${otherUser}')" type="button"  class="Conversation_form_button Conversation_form_submit">
                        <i class="ri-send-plane-2-line"></i>
                    </button>
                </div>
    `;
    document.querySelector('.Conversation_default').classList.remove('active');
    document.querySelector('#Conversation1').classList.add('active');
    //console.log(chat);
    var searchResultsContainer = $('#Conversation1');
    //console.log(searchResultsContainer)
    searchResultsContainer.empty();
    searchResultsContainer.html(chat);

    console.log("----------end displayChat--------------")

}
function closeChat() {
    document.querySelector('.Conversation_default').classList.add('active');
    document.querySelector('#Conversation1').classList.remove('active');
}

function compareDate(dateString) {
    // Convert the input date string to a Date object
    const inputDate = new Date(dateString);

    // Get today's date
    const currentDate = new Date();

    // Format today's date to match the input date format (yyyy-mm-dd)
    const todayFormatted = currentDate.toISOString().split('T')[0];

    // Check if the input date is equal to today's date
    if (dateString === todayFormatted) {
        return "Today";
    }

    // Create a new date object for yesterday
    const yesterday = new Date(currentDate);
    yesterday.setDate(currentDate.getDate() - 1);
    const yesterdayFormatted = yesterday.toISOString().split('T')[0];

    // Check if the input date is equal to yesterday's date
    if (dateString === yesterdayFormatted) {
        return "Yesterday";
    }

    // Get the difference in milliseconds between the input date and today's date
    const differenceInMs = currentDate - inputDate;

    // Calculate the number of days in the difference
    const differenceInDays = Math.floor(differenceInMs / (1000 * 60 * 60 * 24));

    // If the difference is less than 7 days, return the day name of the input date
    if (differenceInDays < 7) {
        const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        const dayIndex = inputDate.getDay();
        const dayName = days[dayIndex];
        return `${dayName}, ${dateString}`;
    }

    // If none of the above conditions are met, return the input date as is
    return dateString;
}

////////////////////////////////////////////


