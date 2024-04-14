"use strict";


const profile_pic = document.querySelector('.profile-pic');
const searchInput = document.querySelector('.nav-content .search-bar .search-input');
const notification_icon = document.querySelector('.nav-content .right-items .items .notification i');
const more_options = document.querySelector('.nav-content .right-items .profile-box .profile-name');

const btn_chat = document.querySelector('.chat');
const btn_group = document.querySelector('.group');
const btn_settings = document.querySelector('.Settings');
const btn_home = document.querySelector('.home');

//! add post !//
const add_post_form = document.querySelector(".add-post-form")
const add_post_img = document.querySelector('.add-post-img');
const add_post_text = document.querySelector('#write-post-text');
const btn_emoji = document.querySelector('.emoji-btn');
const btn_has = document.querySelector('.hash-btn'); 
const btn_select_img = document.querySelector('.img-btn');
const btn_post = document.querySelector('.add-post .right .post-btn');
const privacyDropdown = document.getElementById('privacyDropdown');
const dropdownMenu = privacyDropdown.nextElementSibling;
let privacy_value = 'public';


//! Post Box !//
const post_container = document.querySelector('.posts-container');
const post_time = document.querySelector('.post-time')
const btn_post_options = document.querySelector('.more-options-icon');
const btn_like_icon = document.querySelector('.like-icon');
const like_number = document.querySelector('.like-numbers');
const btn_dislike_icon = document.querySelector('.dislike-icon i');
const dislike_number = document.querySelector('.dislike-numbers');
const btn_repost_icon = document.querySelector('.repost-icon i');
const repost_number = document.querySelector('.repost-numbers')
const btn_comment_icon = document.querySelector('.comment-icon i');
const comment_number = document.querySelector('.comment-numbers');
const btn_add_comment = document.querySelector('.add-comment-icon');
const btn_all_comment_ = document.querySelector('.posts-container .post .box .post-content a');
const postDate = new Date('2024-03-30T12:00:00');
const posted_at = document.querySelector('.post-time');
const views_number = document.querySelector('.post-views');
const post_text = document.querySelector('.post-text');
const post_img = document.querySelector('.post-imgs');
const post_video = document.querySelector('.post-video')
// *Comment =+> 
const comment_text = document.querySelector('.comment-text');
const add_comment = document.querySelector('.add-comment-icon')
const comments_number = document.querySelector('.post-comments');

// Drop Down menu
const logout = document.querySelector(".logout")

document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.querySelector('.search-input');

    searchInput.addEventListener('input', function () {
        const inputValue = searchInput.value.trim();
        if (inputValue.length > 0) {
            searchInput.value = '';
        }
    });
});

//! Emoji Picker Function ==>
const picker = new EmojiButton();
btn_emoji.addEventListener('click', () => {
    picker.togglePicker(btn_emoji);
    console.log("DONE");
});

picker.on('emoji', emoji => {
    add_post_text.value += emoji;
});

//! Auto increase input area
function auto_increase_input_height(element) {
    element.style.height = 'auto';
    element.style.height = (element.scrollHeight) + 'px';
}

window.addEventListener('load', function () {
    var textarea = document.getElementById('write-post-text');
    auto_increase_input_height(textarea);
});

document.getElementById('write-post-text').addEventListener('input', function () {
    auto_increase_input_height(this);
});

// //! Select Image in Add Post  to show when choses!//
btn_select_img.addEventListener('click', function () {
    console.log("clicked")
    const input = document.querySelector('#add-img-post');

    input.addEventListener('change', function (event) {
        const selectedFile = event.target.files[0];
        if (selectedFile) {
            const reader = new FileReader();
            reader.onload = function () {
                add_post_img.src = reader.result;
                console.log('Selected file:', selectedFile);
                console.log('File name:', selectedFile.name);
                console.log('Updated src:', add_post_img.src);
            };
            reader.readAsDataURL(selectedFile);
        }
    });


});




//! Toggle profile menu 

document.addEventListener('DOMContentLoaded', function () {
    const profileBox = document.getElementById('profile-box');
    const dropdownMenu = document.getElementById('dropdown-menu');

    profileBox.addEventListener('click', function (event) {
        event.stopPropagation();
        profileBox.classList.toggle('active');
    });

    // Close the dropdown menu when clicking outside 
    document.addEventListener('click', function () {
        profileBox.classList.remove('active');
    });

    const dropdownItems = document.querySelectorAll('.dropdown-item');
    dropdownItems.forEach(function (item) {
        item.addEventListener('click', function (event) {
            const action = this.getAttribute('data-action');
            console.log('Clicked on:', action);
        });
    });
});


// DropDown Menu Functions --> 
logout.addEventListener("click", () => {

})



//! Change Privacy ====>
// Show or hide the dropdown menu when privacy dropdown is clicked
privacyDropdown.addEventListener('click', function (event) {
    event.stopPropagation();// Stop event propagation

    dropdownMenu.classList.toggle('show');
});

// Close dropdown menu when clicking outside of it or selecting an option
document.addEventListener('click', function (event) {
    if (!event.target.matches('.dropdown-toggle') && !event.target.closest('.dropdown-menu')) {
        dropdownMenu.classList.remove('show');
    }
});

// Set selected privacy option when clicking on a dropdown menu item
dropdownMenu.addEventListener('click', function (event) {
    event.stopPropagation(); // Stop event propagation

    if (event.target.tagName === 'LI') {
        const selectedValue = event.target.getAttribute('data-value');
        privacyDropdown.querySelector('span').textContent = event.target.textContent;

        // Remove selected class from all items
        dropdownMenu.querySelectorAll('li').forEach(item => {
            item.classList.remove('selected');
        });

        // Add selected class to the clicked item
        event.target.classList.add('selected');

        // Update privacy
        privacy_value = selectedValue;
        document.querySelector("#selected-privacy").value = selectedValue;
        dropdownMenu.classList.remove('show');
    }

    console.log(privacy_value)
    event.preventDefault();
});




// ! Add post date func ==>
function calculatePostTime(postDateAttr) {
    const postDate = new Date(postDateAttr);

    //console.log(`THis is the date that i convert to ${postDate} `)
    //console.log(`THis is the date converting ${postDateAttr} `)

    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    const timeDifference = currentDate.getTime() - postDate.getTime();
    const secondsDifference = Math.floor(timeDifference / 1000);
    const minutesDifference = Math.floor(secondsDifference / 60);
    const hoursDifference = Math.floor(minutesDifference / 60);
    const daysDifference = Math.floor(hoursDifference / 24);


    if (daysDifference < 1) {
        if (hoursDifference < 1) {
            if (minutesDifference < 1) {
                return 'Just now';
            } else if (minutesDifference === 1) {
                return '1m';
            } else {
                return `${minutesDifference}m`;
            }
        } else if (hoursDifference === 1) {
            return '1h';
        } else {
            return `${hoursDifference}h`;
        }
    } else if (daysDifference === 1) {
        return '1d';
    } else if (daysDifference < 7) {
        return `${daysDifference}d`;
    } else {
        const options = { year: 'numeric', month: 'short', day: 'numeric' };
        return postDate.toLocaleDateString('en-US', options);
    }
}


function updatePostTime(postElement) {
    const postDateAttr = postElement.getAttribute('data-post-date');
    const timeText = calculatePostTime(postDateAttr);

    const posted_at = postElement.querySelector('.post-time');
    posted_at.textContent = timeText;
}


const postElements = document.querySelectorAll('.post');

postElements.forEach(postElement => {
    updatePostTime(postElement);
});


function isArabic(text) {
    const arabicRange = /[\u0600-\u06FF]/;
    return arabicRange.test(text);
}

// Function to set direction and text alignment based on text language
function setTextDirection(textContainer, text) {
    if (text && isArabic(text.replace(/[^\u0600-\u06FF]/g, ''))) {
        console.log("THE TEXT is Arabic");
        textContainer.style.direction = 'rtl';
        textContainer.style.textAlign = 'right';
    } else {
        console.log("THE TEXT is Not Arabic");
        textContainer.style.direction = 'ltr';
        textContainer.style.textAlign = 'left';
    }
}


///////////////////////////////// Show Followers and Following List ///////////////////////////////////////////////////
// Get Following List
const followingBtn = document.querySelector(".Following");
followingBtn.addEventListener('click', function () {
    fetchFollowing();
});

// Get Followers List
const followersBtn = document.querySelector(".Followers");
followersBtn.addEventListener('click', function () {
    fetchFollowers();
});


// Get All Followers 
function fetchFollowers() {
    var CurrentUserID = document.getElementById('CurrentUserID').value;

    fetch(`/Follow/getAllFollowers?id=${CurrentUserID}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch Followers');
            }
            return response.json();
        })
        .then(List => {
            displayFollowersList(List);

            var modal = document.getElementById('show_followes_list');
            var modalTitle = modal.querySelector('.modal-title');
            modalTitle.textContent = 'Following List';


            var myModal = new bootstrap.Modal(document.getElementById('show_followes_list'));
            myModal.show();
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Get All Following 
function fetchFollowing() {
    var CurrentUserID = document.getElementById('CurrentUserID').value;

    fetch(`/Follow/getAllFollowing?id=${CurrentUserID}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch Following');
            }
            return response.json();
        })
        .then(List => {

            displayFollowingList(List);

            var modal = document.getElementById('show_followes_list');
            var modalTitle = modal.querySelector('.modal-title');
            modalTitle.textContent = 'Follower List';

            var myModal = new bootstrap.Modal(document.getElementById('show_followes_list'));
            myModal.show();
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayFollowersList(model) {
    var modalBody = document.getElementById('FollowersModal');
    console.log("FOLLOWERS==>", model);
    modalBody.innerHTML = '';

    model.forEach(function (user) {
        var userTemplate = `
    <div class="user">
            <div class="profile">
            <input type="hidden" value="${user.userId}" id="FolloweID">
                <img class="profile-pic" src="${user.userImage}" alt="Profile image">
                <div class="name">
                    <p class="username">${user.userName}</p>
                </div>
            </div>
            <div class="follow-button">
                ${user.isFollowing ?
                `<button class="btn btn-follow-following following" onclick="clickOnUnFollowFromList(this, '${user.userId}')">
                        <i class="fa-solid fa-check btn-icon"></i> Following
                    </button>` :
                `<button class="btn btn-follow-following follow" onclick="clickOnFollowFromList(this, '${user.userId}')">
                        <i class="fa-solid fa-user-plus btn-icon"></i> Follow
                    </button>`
            }
            </div>
        </div>
    `;

        // Append the user template to the modal body
        modalBody.insertAdjacentHTML('beforeend', userTemplate);
    });


    //// Going to Follower's Profile 
    const profile = document.querySelectorAll('.modal-dialog .profile');
    profile.forEach(function (item) {
        item.addEventListener('click', function (event) {
            console.log("Click on notification ===>")
            event.preventDefault();

            const userId = item.querySelector("#FolloweID").value;

            const userProfileUrl = `ViewUserProfile?userId=${userId}`;


            window.location.href = userProfileUrl;
        });
    });
}

function displayFollowingList(model) {
    var modalBody = document.getElementById('FollowersModal');
    console.log("FOLLOWING==>", model);
    modalBody.innerHTML = '';

    model.forEach(function (user) {
        var userTemplate = `
    <div class="user">
            <div class="profile">
            <input type="hidden" value="${user.userId}" id="FolloweID">
                <img class="profile-pic" src="${user.userImage}" alt="Profile image">
                <div class="name">
                    <p class="username">${user.userName}</p>
                </div>
            </div>
            <div class="follow-button">
                ${user.isFollowing ?
            `<button class="btn btn-follow-following following" onclick="clickOnUnFollowFromList(this, '${user.userId}')">
                        <i class="fa-solid fa-check btn-icon"></i> Following
                    </button>` :
                `<button class="btn btn-follow-following follow" onclick="clickOnFollowFromList(this, '${user.userId}')">
                        <i class="fa-solid fa-user-plus btn-icon"></i> Follow
                    </button>`
            }
            </div>
        </div>
    `;

        // Append the user template to the modal body
        modalBody.insertAdjacentHTML('beforeend', userTemplate);
    });


    //// Going to Follower's Profile 
    const profile = document.querySelectorAll('.modal-dialog .profile');
    profile.forEach(function (item) {
    item.addEventListener('click', function (event) {
        console.log("Click on notification ===>")
        event.preventDefault();

        const userId = item.querySelector("#FolloweID").value;

        const userProfileUrl = `ViewUserProfile?userId=${userId}`;


        window.location.href = userProfileUrl;
    });
});
}


/////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// Following  /////////////////


function clickOnFollow(followingId) {
    const follow____btn = document.querySelector(".btn-follow-following");
    fetch('/Follow/FollowUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(followingId)
    })
        .then(response => {
            if (response.ok) {
                follow____btn.innerHTML = " ";
                follow____btn.innerHTML = `
                <i class="fa-solid fa-check"></i> Following
                `

            } else {
                console.log("Error")
            }
        })
        .catch(error => {
            console.log("Error")
        });
}
function clickOnUnFollow(followingId) {
    const follow____btn = document.querySelector(".btn-follow-following");
    fetch('/Follow/UnFollowUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(followingId)
    })
        .then(response => {
            if (response.ok) {
                follow____btn.innerHTML = " ";
                follow____btn.innerHTML = `
                 <i class="fa-solid fa-user-plus"></i> Follow
                `
            } else {
                console.log("Error")
            }
        })
        .catch(error => {
            console.log("Error")
        });
}

///////////////////////////////////////////////////////////////////
//// From the list 
//////////////////////////////////// Following  /////////////////


function clickOnFollowFromList(btn,followingId) {
    const follow____btn = document.querySelector(".btn-follow-following");
    fetch('/Follow/FollowUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(followingId)
    })
        .then(response => {
            if (response.ok) {
                btn.innerHTML = " ";
                btn.innerHTML = `
                <i class="fa-solid fa-check"></i> Following
                `

            } else {
                console.log("Error")
            }
        })
        .catch(error => {
            console.log("Error")
        });
}
function clickOnUnFollowFromList(btn,followingId) {
    const follow____btn = document.querySelector(".btn-follow-following");
    fetch('/Follow/UnFollowUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(followingId)
    })
        .then(response => {
            if (response.ok) {
                btn.innerHTML = " ";
                btn.innerHTML = `
                 <i class="fa-solid fa-user-plus"></i> Follow
                `
            } else {
                console.log("Error")
            }
        })
        .catch(error => {
            console.log("Error")
        });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////Load ALL POSTES /////////////////////////////////////////////////////////////////
const timelineCategories = document.querySelectorAll('.timeline .list');

let pageIndex = 0;



function fetchPosts(pageIndex) {
    fetch(`/Profile/GetPosts?pageIndex=${pageIndex}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            return response.json();
        })
        .then(Model => {
            displayPosts(Model);
        })
        .catch(error => {
            console.error('Error :', error);
        });
}
function fetchImages() {
    fetch(`/Profile/GetPostsImages`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }
            return response.json();
        })
        .then(Model => {
            console.log("======================");
            console.log(Model);
        })
        .catch(error => {
            console.error('Error :', error);
        });
}

function displayPostsImages(images) {


    images.forEach(image => {
        let imageHtml = `
            <div class="gallery-item">
                <img src="${image}" alt="Image">
            </div>`;
        post_container.insertAdjacentHTML('beforeend', imageHtml);
    });
}
////////////////////////////////// TIme Line Gallery =====> 
timelineCategories.forEach(category => {
    category.addEventListener('click', function () {
        const filter = this.dataset.filter;

        timelineCategories.forEach(cat => {
            cat.classList.remove('active');
        });

        this.classList.add('active');


        fetchContent(filter);
    });
});

function fetchImages() {
    fetch(`/Profile/GetPostsImages`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch images');
            }
            return response.json();
        })
        .then(images => {
            displayImagesInGallery(images);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayImagesInGallery(images) {
    const galleryContainer = document.createElement('div');
    galleryContainer.classList.add('gallery');

    images.forEach(image => {
        const imageElement = document.createElement('img');
        imageElement.src = image;
        imageElement.alt = 'Photo';
        galleryContainer.appendChild(imageElement);
    });

    post_container.appendChild(galleryContainer);
}
function fetchContent(filter) {

    if (filter === 'ALL') {
        post_container.innerHTML = "";
        fetchPosts(pageIndex);
        console.log("All")

    } else if (filter === 'Photos') {
        post_container.innerHTML = "";
        fetchImages()
        console.log("Images")

    }
}



// Function to display posts
function displayPosts(Model) {
    Model.posts.forEach(post => {
        const isCurrentUserPost = post.user.id === Model.currentUserId;
        let postHtml = `
            <div class="post" data-post-date="Just now">
                <div class="box">
                           <input type="hidden" class="post-id" value="${post.id}">
                    <div class="top">
                        <!-- Profile -- views -->
                        <div class="profile post-profile">
                                 <input type="hidden" value="${post.user.id}" id="PostID">
                            <img class="profile-pic" src="${post.user.personalImage}"  alt="Profile image">
                            <div class="name">
                                <p class="username">${post.user.userName} <i class="bi bi-patch-check-fill text-primary"></i> </p>
                                <!-- Using js function to calculate the time -->
                                <p class="post-time">${post.postAddedTime}</p>
                            </div>
                        </div>
                          ${isCurrentUserPost ? `
                    <div class="delete-post" onclick="deletePost(${post.id})">
                        <i class="fa-solid fa-trash delete-icon text-danger"></i>
                    </div>
                ` : ''}
                    </div>
                    <!-- Post Content -->
                    <div id="post${post.id}" class="post-content">
                        ${post.textContent ? `<p>${post.textContent}</p>` : ''}
                        <div class="post-img">
                            ${post.postImage ? `<img src="${post.postImage}" alt="Post Image">` : ''}
                        </div>
                        <div class="interactions">
                        <div class="interactions-container">
                              <div class="box">
                                <i id="likePost${post.id}" class="fa-solid fa-heart like-icon" style="color: grey" onclick="lovePost(${post.id}, true)"></i>
                                <span id="likePostCount${post.id}">${post.likeCount}</span>
                            </div>
                            <div class="box">
                                <i id="dislikePost${post.id}" class="fa-solid fa-thumbs-down dislike-icon" style="color: grey" onclick="lovePost(${post.id}, false)"></i>
                                <span id="dislikePostCount${post.id}">${post.dislikeCount}</span>
                            </div>
                            <!--
                            <div class="box">
                                <i class="fa-solid fa-retweet repost-icon" onclick="rePost('${post.id}')"></i>
                                <span>${post.repostCount}</span>
                            </div>
                            -->
                            <div class="box">
                                <i class="fa-solid fa-comment comment-icon" onclick="getPostComments(${post.id})"></i>
                                <span id="postCommentCount${post.id}">${post.commentCount}</span>
                            </div>
                        </div>

                              <div class="box">
    <i class="bi bi-bookmark-fill bookmark-icon" onclick="addBookmark('${post.id}')"></i>
</div>

                        </div>
                        ${post.commentCount > 2 ? `<a href="#">View <span>${post.commentCount}</span> Comments</a>` : ''}
                    </div>
                    <!-- Add Comment -->
                    <div class="add-comment">
                        <img class="profile-pic" src="${Model.currentUserImage}" alt="">
                        <input id="postId${post.id}" type="text" placeholder="Add a comment">
                        <i class="fa-solid fa-hand-pointer add-comment-icon" onclick="addNewComment(${post.id})""></i>
                    </div>
                </div>
            </div>`;

        post_container.insertAdjacentHTML('beforeend', postHtml);
    });
}





function loadMore() {
    const lastPost = document.querySelector('.post:last-of-type');
    if (lastPost) {
        const theLastPost = lastPost.getBoundingClientRect().bottom - window.innerHeight;
        const threshold = 300;
        if (theLastPost < threshold) {
            pageIndex++;
            fetchPosts(pageIndex);
        }
    }
}

window.addEventListener('scroll', loadMore);

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// * All Post Functionally * 
// Function to handle liking a post
function likePost(postId) {
}

// Function to handle adding a comment to a post
function addComment(postId, commentText) {
}

// Event  for liking a post
if (btn_like_icon !== null) {

    btn_like_icon.addEventListener('click', function () {
        let postId;
        likePost(postId);
    });
}

// Event  for adding a comment to a post
if (btn_add_comment !== null) {

    btn_add_comment.addEventListener('click', function () {
        let postId;
        let commentText;
        addComment(postId, commentText);
    });
}

// Function to update the number of likes on a post
function updateLikes(postId, numberOfLikes) {
}

// Function to update the number of comments on a post
function updateComments(postId, numberOfComments) {
}



////////////////////////////////////////////////// SignalR //////////////////////////////////////////////////////////////
document.addEventListener("DOMContentLoaded", function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/PostHub").build();

    connection.start().then(function () {
        console.log("SignalR connection established.");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    /// Send the data from the form
    document.getElementById("postForm").addEventListener("submit", function (event) {
        event.preventDefault();

        var formData = new FormData(this);
        console.log("THis ==>")
        console.log(this)

        fetch('/Posts/CreatePost', {
            method: 'POST',
            body: formData
        })
            .then(() => {
                console.log("Data send successfully")
            }).catch(() => {
                console.log("Error")
            })


    });


    ////// Consume the data 


    connection.on("NewPostCreated", function (post) {
        let PostHtml = `
                    <div class="post" data-post-date="Just now">
                        <div class="box">
                           <input type="hidden" class="post-id" value="${post.id}">
                            <div class="top">
                                <!-- Profile -- views -->
                                <div class="profile">
                                    <img class="profile-pic" src= "${post.user.personalImage}"  alt="Profile image">
                                    <div class="name">
                                        <p class="username">${post.user.userName} <i class="bi bi-patch-check-fill text-primary"></i> </p>
                                        <!-- Using js function to calculate the time -->
                                        <p class="post-time">Just now</p>
                                    </div>
                                </div>
                    <div class="delete-post" onclick="deletePost(${post.id})">
                        <i class="fa-solid fa-trash delete-icon text-danger"></i>
                    </div>
                    </div>
                            <!-- Post Content -->
                            <div class="post-content">
                                ${post.textContent ? `<p>${post.textContent}</p>` : ''}
                                <div class="post-img">
                                    ${post.postImage ? `<img src="${post.postImage}" alt="Post Image">` : ''}
                                </div>
                        <div class="interactions">
                        <div class="interactions-container">
                              <div class="box">
                                <i id="likePost${post.id}" class="fa-solid fa-heart like-icon" style="color: grey" onclick="lovePost(${post.id}, true)"></i>
                                <span id="likePostCount${post.id}">${post.likeCount}</span>
                            </div>
                            <div class="box">
                                <i id="dislikePost${post.id}" class="fa-solid fa-thumbs-down dislike-icon" style="color: grey" onclick="lovePost(${post.id}, false)"></i>
                                <span id="dislikePostCount${post.id}">${post.dislikeCount}</span>
                            </div>
                                 <!--
                            <div class="box">
                                <i class="fa-solid fa-retweet repost-icon"></i>
                                <span>${post.repostCount}</span>
                            </div>
                            -->
                            <div class="box">
                                <i class="fa-solid fa-comment comment-icon" onclick="getPostComments(${post.id})"></i>
                                <span id="postCommentCount${post.id}">${post.commentCount}</span>
                            </div>
                        </div>

                                <i class="bi bi-bookmark-fill"></i>
                        </div>
                                ${post.commentCount > 2 ? `<a href="#">View <span>${post.commentCount}</span> Comments</a>` : ''}
                            </div>
                            <!-- Add Comment -->
                            <div class="add-comment">
                                <img class="profile-pic" src="${post.user.personalImage}" alt="">
                                <input id="postId${post.id}" type="text" placeholder="Add a comment">
                                <i class="fa-solid fa-hand-pointer add-comment-icon" onclick="addNewComment(${post.id})"></i>
                            </div>
                        </div>
                    </div>

                        
                `;

        post_container.insertAdjacentHTML('afterbegin', PostHtml);
        console.log("Comments COunt " + post.commentCount)
        const newPostElement = post_container.querySelector('.post');
        newPostElement.classList.add('hidden');
        void newPostElement.offsetWidth;
        newPostElement.classList.remove('hidden');
        newPostElement.classList.add('visible');

        add_post_text.value = '';
        dropdownMenu.querySelector('[data-value="public"]').classList.add('selected');
        dropdownMenu.querySelector('[data-value="friends"]').classList.remove('selected');
        dropdownMenu.querySelector('[data-value="private"]').classList.remove('selected');

        add_post_img.src = '';
        document.querySelector('#add-img-post').value = "";
        //        const privacyValue = selectedPrivacy ? selectedPrivacy.dataset.value : 'public';
        //privacy_value = privacyValue;

        console.log("==================TEST ==========================")
        console.log(post)
        console.log("OUT")

    })

});

function lovePost(postId, likeOption) {
    console.log("Love post");
    let likePost = $("#likePost" + postId);
    let dislikePost = $("#dislikePost" + postId);

    let likesCount = $("#likePostCount" + postId);
    let dislikesCount = $("#dislikePostCount" + postId);
    console.log(likePost)
    console.log(dislikePost)


    $.ajax({
        url: `/posts/InteractPost?PostId=${postId}&LikeOption=${likeOption}`,
        type: 'POST',
        //data: {commentId:commentId  }, // Include the postId and commentText parameters
        success: function (response) {
            if (likeOption == true) {
                if (likePost.css('color') == "rgb(128, 128, 128)") {
                    likePost.css('color', "red");
                    dislikePost.css('color', "grey");
                }
                else {
                    likePost.css('color', "grey");
                }
            }
            else {
                console.log("Not if")
                if (dislikePost.css('color') == "rgb(128, 128, 128)") {
                    dislikePost.css('color', "black");
                    likePost.css('color', "grey");
                }
                else {
                    dislikePost.css('color', "grey");
                }
            }
            console.log(response);
            likesCount.text(response['likes'])
            dislikesCount.text(response['dislikes'])
        },
        error: function (xhr, status, error) {
        }
    });
}
function getPostComments(postId) {
    console.log(postId);
    console.log("Post id" + postId);
    console.log("Get Post Comments");
    $('#commentsModal').html("");
    $.ajax({
        url: `/comments/getComments?postId=` + postId, // Endpoint URL
        type: 'GET',
        //data: { postId: postId, commentText: commentText }, // Include the postId and commentText parameters
        success: function (response) {
            // Handle the success response from the server
            console.log('AJAX request successful');
            console.log(response);
            let a = ``;
            $('#commentsModal').append(a);
            for (let comment of response) {

                let profileImage = '/images/';
                profileImage += comment.userProfileImage;
                console.log(comment);
                console.log(profileImage);
                a = `<div id="${comment.commentId}" class="comment">
                                        <div class="profile">
                                                        <img class="profile-pic" src=${profileImage} alt="Profile image">
                                            <div class="name">
                                                        <p class="username">${comment.userFirstName} ${comment.userLastName}</p>
                                                <p class="comment-time">${comment.commentDate}o</p>
                                            </div>
                                        </div>
                                        <div class="comment-content">
                                            <p class="comment-text">${comment.commentText}</p>
                                        </div>
                                        <div class="love-icon">
                                                <i id="likeIcon${comment.commentId}" class="fa-solid fa-heart like-icon" style="color: ${comment.isLikedByMe == true ? 'red' : 'grey'}" onclick="likeComment(${comment.commentId})"></i>
                                                <span id="likeCommentIcon${comment.commentId}">${comment.likeCount}</span>
                                        </div>
                                    </div>`
                $('#commentsModal').append(a);
            }
            $(`#postCommentCount` + postId).text(response.length);
            $('#show_comments').modal('show');
        },
        error: function (xhr, status, error) {
            // Handle errors
            console.error('AJAX request failed');
            console.error(xhr.responseText);
        }
    });
}






function addNewComment(PostId) {
    console.log("Works");
    let commentText = $(`#postId` + PostId).val(); // Get the commentText value from the input field
    $(`#postId` + PostId).val("");

    if (commentText == "")
        return;

    let commentsViewsCount = $(`#postCommentCount` + PostId).text();
    console.log(commentsViewsCount);
    $(`#postCommentCount` + PostId).text(parseInt($(`#postCommentCount` + PostId).text()) + 1)


    $.ajax({
        url: `/comments/newcomment?postId=${PostId}&CommentText=${commentText}`, // Endpoint URL
        type: 'POST',
        //data: { postId: PostId, commenttext: commentText }, // Include the postId and commentText parameters

        success: function (response) {
            // Handle the success response from the server
            console.log('AJAX request successful');
            console.log(response);
        },
        error: function (xhr, status, error) {
            // Handle errors
            console.error('AJAX request failed');
            console.error(xhr.responseText);
            $(`#postCommentCount` + PostId).text(parseInt($(`#postCommentCount` + PostId).text()) - 1)
            //(`#postCommentCount` + PostId).val() = numOfComments - 1;
        }
    });
}


//asd
function likeComment(commentId) {
    console.log(commentId);
    var color = $(`#likeIcon${commentId}`).css('color');
    console.log(color)
    if (color == 'rgb(128, 128, 128)') {
        console.log("From grey to red");
        $(`#likeIcon${commentId}`).css('color', 'red');
        $(`#likeCommentIcon` + commentId).text(parseInt($(`#likeCommentIcon` + commentId).text()) + 1)
    }
    else {
        console.log("From red to grey");
        $(`#likeIcon${commentId}`).css('color', 'grey');
        $(`#likeCommentIcon` + commentId).text(parseInt($(`#likeCommentIcon` + commentId).text()) - 1)
    }

    $.ajax({
        url: `/comments/LikeComment?commentId=${commentId}`,
        type: 'POST',
        //data: {commentId:commentId  }, // Include the postId and commentText parameters
        success: function (response) {
        },
        error: function (xhr, status, error) {
            if (color == 'red') {
                $(`#likeIcon${commentId}`).css('color', 'grey');
                $(`#likeCommentIcon` + commentId).text(parseInt($(`#likeCommentIcon` + commentId).text()) - 1)
            }
            else {
                $(`#likeIcon${commentId}`).css('color', 'red');
                $(`#likeCommentIcon` + commentId).text(parseInt($(`#likeCommentIcon` + commentId).text()) + 1)
            }
        }
    });
}




///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////    Get Notifications  //////////////////////////////////////////////////////////

const notificationCountElement = document.querySelector('.notification-count');

/////////////////////// Notification SignalR /////////////////////////////////////
const notificationConnection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();

notificationConnection.start()
    .then(() => console.log("Connected to the notification Hub"))
    .catch(err => console.log("Error =>", err.toString()))

notificationConnection.on("ReceiveFollowNotification", () => {
    //alert("GOOOOOOOOOOOOOOOOOOOT Follow 🔥🔥🔥")
    NewNotificationAriived();
});

notificationConnection.on("ReceiveUnfollowNotification", () => {
    NewNotificationAriived();

});


function NewNotificationAriived() {

    if (parseInt(notificationCountElement.textContent) === 9) {

        notificationCountElement.textContent = '+9 ';
    } else {
        const newCount = Number(notificationCountElement.textContent) + 1;
        notificationCountElement.textContent = newCount;
    }
    notificationCountElement.style.display = 'block';



}

////////////////////////////////////// Notification Toggle Menu //////////////////////////////////////
// Notification menu
document.getElementById('notification-icon').addEventListener('click', function (event) {
    event.stopPropagation();
    document.getElementById('notification-container').classList.toggle('active');

    /// Get Notifications =>

    fetch('/HomePage/GetNotifications')
        .then(res => {
            if (res.ok) {
                return res.json();
            } else {
                console.log("Error in response");
            }
        })
        .then(notificationList => {
            //console.log("-------------->" + notificationList);
            ShowNotifications(notificationList);
        })
        .catch(err => console.log(err.toString()))

});



const notificationList = document.querySelector(".notification-list")
/////////////////// Get and show notifications when click on the notification icon /////////////////////////////

function ShowNotifications(notifications) {
    console.log(notifications);
    //// Remove the new notification number -->
    notificationCountElement.textContent = 0;
    notificationCountElement.style.display = 'none'


    notificationList.innerHTML = " ";
    notifications.forEach(notification => {
        const HTML = `
<div class="notification-item ${notification.isSeen ? 'seen' : 'unseen'}">
        <input type="hidden" name="notificationUserId" value="${notification.createdByUserId}">
        ${notification.content === 'followed you' ? '<i class="fa-solid fa-user-plus text-primary"></i>' : ''}
        ${notification.content === 'unfollowed your profile' ? '<i class="fa-solid fa-user-xmark text-danger"></i>' : ''}
        ${notification.content === 'loved your post' ? '<i class="fa-solid fa-heart text-danger"></i>' : ''}
        ${notification.content === 'commented on your post' ? '<i class="fa-solid fa-comment-alt text-primary"></i>' : ''}
        ${notification.content === 'disliked your post' ? '<i class="fa-solid fa-thumbs-down text-warning"></i>' : ''}
        ${notification.content === 'sent you a message' ? '<i class="fa-solid fa-envelope text-info"></i>' : ''}
        <img src="${notification.userImage}" alt="User" class="user-avatar">
        <div class="notification-details">
            <span class="user-name"><span>${notification.userName}</span></span>
            <span class="notification-text">${notification.content} <br><strong>${notification.notificationShowDate}</strong></span>
        </div>
    </div>
`;

        notificationList.insertAdjacentHTML('afterbegin', HTML);
    });


    const notificationItems = document.querySelectorAll('.notification-item');

    notificationItems.forEach(function (item) {
        item.addEventListener('click', function (event) {
            console.log("Click on notification ===>")
            event.preventDefault();

            const userId = item.querySelector('input[name="notificationUserId"]').value;

            const userProfileUrl = `ViewUserProfile?userId=${userId}`;

            // Navigate to the user profile page
            window.location.href = userProfileUrl;
        });
    });
}

//////////// Delete functionality ==> ////////////////////////////////////////////
// Function to delete a post
function deletePost(postId) {
    const model = `
        <div class="modal fade" id="deletePostModal" tabindex="-1" aria-labelledby="deletePostModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="deletePostModalLabel">Confirm Deletion</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body fw-bold text-danger" style="    height: fit-content;overFlow:hidden;">
                Are you sure you want to delete this post?
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                <button id="confirmDeleteBtn" type="button" class="btn btn-danger">Delete</button>
              </div>
            </div>
          </div>
        </div>
    `;

    document.body.insertAdjacentHTML('beforeend', model);



    const deleteModal = new bootstrap.Modal(document.getElementById('deletePostModal'));
    deleteModal.show();


    const confirmBtn = document.getElementById('confirmDeleteBtn');
    confirmBtn.addEventListener('click', function () {
        fetch(`/Posts/Delete?id=${postId}`, {
            method: 'DELETE',
        })
            .then(response => {
                if (response.ok) {
                    const postIdInput = document.querySelector(`input.post-id[value="${postId}"]`);


                    if (postIdInput) {
                        const postId = postIdInput.value;

                        const postElement = postIdInput.closest('.post');
                        if (postElement) {
                            console.log(postElement)
                            postElement.remove();
                        }
                    }

                } else {
                    console.error(`Failed to delete post with ID ${postId}.`);
                }
            })
            .catch(error => {
                console.error(`Error deleting post with ID ${postId}:`, error);
            });

        deleteModal.hide();
    });
}

