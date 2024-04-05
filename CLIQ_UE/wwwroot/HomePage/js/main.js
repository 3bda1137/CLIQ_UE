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
    const btn_has = document.querySelector('.hash-btn'); // Corrected selector
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


    // Notification menu
    document.getElementById('notification-icon').addEventListener('click', function (event) {
        event.stopPropagation();
        document.getElementById('notification-container').classList.toggle('active');
    });

    document.body.addEventListener('click', function (event) {
        if (!event.target.closest('#notification-container')) {
            document.getElementById('notification-container').classList.remove('active');
        }
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

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////Load ALL POSTES /////////////////////////////////////////////////////////////////
let pageIndex = 0;

fetchPosts(pageIndex);
function fetchPosts(pageIndex) {
    fetch(`/HomePage/getPosts?pageIndex=${pageIndex}`)
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



// Function to display posts
function displayPosts(Model) {
    console.log("Model posts")
    console.log(Model)
        console.log(Model.posts)
    Model.posts.forEach(post => {
        let postHtml = `
            <div class="post" data-post-date="Just now">
                <div class="box">
                    <div class="top">
                        <!-- Profile -- views -->
                        <div class="profile">
                            <img class="profile-pic" src="${post.user.personalImage}"  alt="Profile image">
                            <div class="name">
                                <p class="username">${post.user.userName} <i class="bi bi-patch-check-fill text-primary"></i> </p>
                                <!-- Using js function to calculate the time -->
                                <p class="post-time">${post.postAddedTime}</p>
                            </div>
                        </div>
                        <div class="views">
                            <div class="views-number">
                                <i class="fa-solid fa-eye"></i>
                                <p>${post.viewsCount}</p>
                            </div>
                            <div class="more-options">
                                <i class="fa-solid fa-ellipsis more-options-icon"></i>
                            </div>
                        </div>
                    </div>
                    <!-- Post Content -->
                    <div class="post-content">
                        ${post.textContent ? `<p>${post.textContent}</p>` : ''}
                        <div class="post-img">
                            ${post.postImage ? `<img src="${post.postImage}" alt="Post Image">` : ''}
                        </div>
                        <div class="interactions">
                            <div class="box">
                                <i class="fa-solid fa-heart like-icon"></i>
                                <span>${post.likeCount}</span>
                            </div>
                            <div class="box">
                                <i class="fa-solid fa-thumbs-down dislike-icon"></i>
                                <span>${post.dislikeCount}</span>
                            </div>
                            <div class="box">
                                <i class="fa-solid fa-retweet repost-icon"></i>
                                <span>${post.repostCount}</span>
                            </div>
                            <div class="box">
                                <i class="fa-solid fa-comment comment-icon" onclick="getPostComments(${post.id})"></i>
                                <span>${post.commentCount}</span>
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
    const theLastPost = lastPost.getBoundingClientRect().bottom - window.innerHeight;
    const threshold = 300;
    if (theLastPost < threshold) {
        pageIndex++;
        fetchPosts(pageIndex);
    }
}

window.addEventListener('scroll', loadMore);
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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


            //fetch('/Posts/CreatePost', {
            //    method: 'POST',
            //    body: formData
            //})
            //    .then(function (response) {
            //        console.log(response)
            //        if (!response.ok) {
            //            throw new Error('Network erroe');
            //        }
            //        return response.json();
            //    })
            //    .then(function (data) {
            //        console.log(" created successfully:", data);

            //    })
            //    .catch(function (error) {
            //        console.error("Error:", error);
            //    });
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
            console.log("Data from send when the event fire")
            console.log(post)

            let PostHtml = `
                    <div class="post" data-post-date="Just now">
                        <div class="box">
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
                                <div class="views">
                                    <div class="views-number">
                                        <i class="fa-solid fa-eye"></i>
                                        <p>${post.viewsCount}</p>
                                    </div>
                                    <div class="more-options">
                                        <i class="fa-solid fa-ellipsis more-options-icon"></i>
                                    </div>
                                </div>
                            </div>
                            <!-- Post Content -->
                            <div class="post-content">
                                ${post.textContent ? `<p>${post.textContent}</p>` : ''}
                                <div class="post-img">
                                    ${post.postImage ? `<img src="${post.postImage}" alt="Post Image">` : ''}
                                </div>
                                <div class="interactions">
                                    <div class="box">
                                        <i class="fa-solid fa-heart like-icon"></i>
                                        <span>${post.likeCount}</span>
                                    </div>
                                    <div class="box">
                                        <i class="fa-solid fa-thumbs-down dislike-icon"></i>
                                        <span>${post.dislikeCount}</span>
                                    </div>
                                    <div class="box">
                                        <i class="fa-solid fa-retweet repost-icon"></i>
                                        <span>${post.repostCount}</span>
                                    </div>
                                    <div class="box">
                                        <i class="fa-solid fa-comment comment-icon" data-bs-toggle="modal" data-bs-target="#show_comments"></i>
                                        <span>${post.commentCount}</span>
                                    </div>
                                </div>
                                ${post.commentCount > 2 ? `<a href="#">View <span>${post.commentCount}</span> Comments</a>` : ''}
                            </div>
                            <!-- Add Comment -->
                            <div class="add-comment">
                                <img class="profile-pic" src="${post.user.personalImage}" alt="">
                                <input type="text" placeholder="Add a comment">
                                <i class="fa-solid fa-hand-pointer add-comment-icon"></i>
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
