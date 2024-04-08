
var otherUser = 0;
function showChat(otherUserId) {
    otherUser = otherUserId;
    console.log("----------------------------")
    console.log(otherUserId)
    console.log("----------------------------")

    $.ajax({
        url: '/chat/GetMessages',
        type: 'GET',
        data: { otherUserId: otherUserId },
        success: function (data) {

            console.log(data);
            displayChat(data);
        },
        error: function () {
            console.error('Error occurred while fetching search results.');
        }
    });
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
    let msg = "";
    let chat = "";
    $.each(results, function (index, result) {
        console.log("length -----= ", results.length)
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
        if (index + 1< results.length) { 
            if (results[index].createdAt != results[index + 1].createdAt) {
                msg +=
                    `
                    <div class="Conversation_Divider">
                        <span>
                            ${results[index].createdAt}
                        </span>
                    </div>
                    `
            }
        }
    });
    chat = `
                <div class="Conversation_Top">
                    <button onclick="closeChat()" type="button" class="Conversation_Back"> <i class="ri-arrow-left-line"></i></button>
                    <div class="Conversation_User">
                        <img  src="/chat/images/test.jpg" class="Conversation_User_Image">
                        <div class="">
                            <div class="Conversation_User_Name">Manar</div>
                            <div class="Conversation_User_Status online">Online</div>
                        </div>
                    </div>
                    <div class="Conversation_buttons">
                        <button type="button"><i class="ri-phone-fill"></i></button>
                        <button type="button"><i class="ri-vidicon-line"></i></button>
                        <button type="button"><i class="ri-information-line"></i></button>

                    </div>
                </div>
                <div class="Conversation_Main">
                    <ul class="Conversation_Wrapper">
                        
                        ${msg}
                        
                    </ul>
                </div>
                <div class="Conversation_form">
                    <button type="button" class="Conversation_form_button" id="emoji-button">
                        <i class="ri-emotion-line"></i>
                    </button>
                    <div class="Conversation_form_group">
                        <textarea class="Conversation_form_input" rows="1" placeholder="Message"> </textarea>
                        <button type="button" class="Conversation_form_Record">
                            <i class="ri-mic-line"></i>
                        </button>
                    </div>
                    <button type="button"  class="Conversation_form_button Conversation_form_submit">
                        <i class="ri-send-plane-2-line"></i>
                    </button>
                </div>
    `;
    document.querySelector('.Conversation_default').classList.remove('active');
    document.querySelector('#Conversation1').classList.add('active');
    console.log(chat);
    var searchResultsContainer = $('#Conversation1');
    console.log(searchResultsContainer)
    searchResultsContainer.empty();
    searchResultsContainer.html(chat);
}
function closeChat() {
    document.querySelector('.Conversation_default').classList.add('active');
    document.querySelector('#Conversation1').classList.remove('active');
}
