$(document).ready(function () {
    $('#searchTerm').on('change', function () {
        var searchTerm = $(this).val();
            makeAjaxCall(searchTerm);
    });
});

function makeAjaxCall(searchTerm) {
    $.ajax({
        url: '/chat/Search',
        type: 'GET',
        data: { searchTerm: searchTerm },
        success: function (data) {

            console.log(data);
            displaySearchResults(data);
        },
        error: function () {
            console.error('Error occurred while fetching search results.');
        }
    });
}

function displaySearchResults(results) {
    var searchResultsContainer = $('#myFollowingUsers');
    searchResultsContainer.empty();
    $.each(results, function (index, result) {
        console.log("###################");
        console.log(result);
        console.log("###################");
        var user =
            `
    <li id="${results[index].userId}">
        <div onclick="showChat('${results[index].userId}','${results[index].userName}','${results[index].imageUrl}')" data-conversation="#Conversation1">
            <img class="Content_Message_image" src="${results[index].imageUrl}">
            <span class="Content_Message_info">
                <span class="Content_Message_Name">${results[index].userName}</span>
                ${results[index].lastMessage ? `<span class="Content_Message_Text">${results[index].lastMessage.message}</span>` : ''}
            </span>
            <span class="Content_Message_More">
                
                <span class="Content_Message_Time">${results[index].formatedTime}</span>
            </span>
        </div>                     
    </li>
    `;

        console.log(user);
        searchResultsContainer.append(user); 
    });
}


