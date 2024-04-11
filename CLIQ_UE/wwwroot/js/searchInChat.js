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

        var user =
            `
    <li>
        <div onclick="showChat('${results[index].userId}')">
            <img class="Content_Message_image" src="${results[index].imageUrl}">
            <span class="Content_Message_info">
                <span class="Content_Message_Name">${results[index].userName}</span>
                ${results[index].lastMessage ? `<span class="Content_Message_Text">${results[index].lastMessage.message}</span>` : ''}
            </span>
            <span class="Content_Message_More">
                <span class="Content_Message_unread">5</span>
                <span class="Content_Message_Time">12:41</span>
            </span>
        </div>                     
    </li>
    `;

        console.log(user);
        searchResultsContainer.append(user); 
    });
}