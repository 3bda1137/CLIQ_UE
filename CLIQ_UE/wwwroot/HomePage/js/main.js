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

// //! Select Image in Add Post !//
btn_select_img.addEventListener('click', function () {
    const input = document.querySelector('#add-img-post');

    input.addEventListener('change', function (event) {
        const selectedFile = event.target.files[0];
        if (selectedFile) {
            const reader = new FileReader();
            reader.onload = function () {
                add_post_img.src = reader.result;
                //console.log('Selected file:', selectedFile);
                //console.log('File name:', selectedFile.name);
                //console.log('Updated src:', add_post_img.src);
            };
            reader.readAsDataURL(selectedFile);
        }
    });


});



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
        // Close dropdown menu
        dropdownMenu.classList.remove('show');
    }

    console.log(privacy_value)
    // Prevent form submission
    event.preventDefault();
});


// ! Add post date func ==>
function calculatePostTime(postDate) {
  const currentDate = new Date();
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
  const postDateAttr = postElement.dataset.postDate;
  const postDate = new Date(postDateAttr);
  const timeText = calculatePostTime(postDate);
  const posted_at = postElement.querySelector('.post-time');
  posted_at.textContent = timeText;
}

const postElements = document.querySelectorAll('.post');

postElements.forEach(postElement => {
  updatePostTime(postElement);
});

// Will use this in foreach --->
//setTextDirection(post_text, post_text.textContent.trim());



// ! Check If the post Arabic or not to change direction

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



// * All Post Functionally * 
// Function to handle liking a post
function likePost(postId) {
}

// Function to handle adding a comment to a post
function addComment(postId, commentText) {
}

// Event  for liking a post
btn_like_icon.addEventListener('click', function () {
  let postId;
  likePost(postId);
});

// Event  for adding a comment to a post
btn_add_comment.addEventListener('click', function () {
  let postId;
  let commentText;
  addComment(postId, commentText);
});

// Function to update the number of likes on a post
function updateLikes(postId, numberOfLikes) {
}

// Function to update the number of comments on a post
function updateComments(postId, numberOfComments) {
}

// Function to add a new post 
        //async function addPost() {
        //    const postContent = add_post_text.value.trim();
        //    if (postContent || add_post_img.src) {
        //        const selectedPrivacy = dropdownMenu.querySelector('.selected');
        //        const privacyValue = selectedPrivacy ? selectedPrivacy.dataset.value : 'public';
        //        const currentDate = new Date();
        //        const postImageSrc = add_post_img.src;

        //        // Convert the image to Base64
        //        const postImageBase64 = await getBase64FromImageUrl(postImageSrc);

        //        // Send the data to action
        //        const postData = {
        //            postContent: postContent,
        //            privacyValue: privacyValue,
        //            postImageSrc: postImageSrc,
        //            postImageBase64: postImageBase64 // Add this property
        //        };
        //        console.log(postData);
        //        try {
        //            const response = await fetch('/Posts/CreatePost', {
        //                method: 'POST',
        //                headers: {
        //                    'Content-Type': 'application/json'
        //                },
        //                body: JSON.stringify(postData)
        //            });

        //            if (response.ok) {

        //                console.log(' successfully!');
        //            } else {
        //                console.error('Error :', response.statusText);
        //            }
        //        } catch (error) {
        //            console.error('Network error:', error);
        //        }

        //        add_post_text.value = '';
        //        add_post_img.src = '';
        //    }
        //}

        //// Function to convert image to Base64
        //function getBase64FromImageUrl(imageUrl) {
        //    return new Promise((resolve, reject) => {
        //        const img = new Image();
        //        img.setAttribute('crossOrigin', 'anonymous');
        //        img.onload = () => {
        //            const canvas = document.createElement('canvas');
        //            canvas.width = img.width;
        //            canvas.height = img.height;
        //            const ctx = canvas.getContext('2d');
        //            ctx.drawImage(img, 0, 0);
        //            const dataURL = canvas.toDataURL('image/png');
        //            resolve(dataURL.replace(/^data:image\/(png|jpg);base64,/, ''));
        //        };
        //        img.onerror = error => reject(error);
        //        img.src = imageUrl;
        //    });
        //}


        //// Event  for the "Post" button
        //btn_post.addEventListener('click', addPost);


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









