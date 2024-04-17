document.addEventListener('DOMContentLoaded', function () {
    var isCoverDefault = false;
    var defaultImageSrc; // Define defaultImageSrc in a broader scope

    function uploadDefaultCoverImage(imageSrc) {
        document.querySelector('.default-cover-images').innerHTML = '';
        var imgDiv = document.createElement('div');
        imgDiv.className = 'col-12 mb-3';
        var img = document.createElement('img');
        img.src = imageSrc;
        img.className = 'img-fluid rounded default-cover-image';
        img.alt = 'Selected Image';
        imgDiv.appendChild(img);
        document.querySelector('.default-cover-images').appendChild(imgDiv);
        attachDefaultCoverImageListeners(); // Reattach event listeners after adding default images
        defaultImageSrc = imageSrc; // Set defaultImageSrc when a default image is selected
        isCoverDefault = true;
    }

    // Attach event listeners to default images
    function attachDefaultCoverImageListeners() {
        var defaultImages = document.querySelectorAll('.default-cover-images .default-cover-image');
        defaultImages.forEach(function (image) {
            image.addEventListener('click', function () {
                var defaultImageSrc = this.getAttribute('src');
                uploadDefaultCoverImage(defaultImageSrc);
            });
        });
    }

    attachDefaultCoverImageListeners(); // Attach event listeners initially

    // Upload button click event handler
    document.getElementById('uploadCoverBtn').addEventListener('click', function () {
        var formData = new FormData();

        if (!isCoverDefault) {
            var fileInput = document.getElementById('uploadCoverInput');
            var file = fileInput.files[0];
            formData.append('image', file);
        } else {
            formData.append('defaultImageSrc', defaultImageSrc);
        }
        console.log("---------------------------------------------------")
        console.log(formData)

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/EditProfile/ChangeCoverImage');
        xhr.onload = function () {
            if (xhr.status === 200) {
                var responseData = JSON.parse(xhr.responseText);
                displayMessageInChangeCoverImage(responseData)
            } else {
                console.error(xhr.responseText);
            }
        };
        xhr.send(formData);
    });
    // Change event handler for file input
    document.getElementById('changeCoverImage').addEventListener('click', function (event) {
        document.getElementById('uploadCoverInput').addEventListener('change', function (event) {
            var file = event.target.files[0];
            var reader = new FileReader();

            reader.onload = function (event) {
                var imageUrl = event.target.result;
                document.querySelector('.default-cover-images').innerHTML = '';
                document.getElementById('uploadedCoverImage').src = imageUrl;
                document.getElementById('uploadedCoverImage').style.display = 'block';
            };
            reader.readAsDataURL(file);
        });
    });

    // Click event handler for "Show All Images" button
    document.getElementById('showAllCoverBtn').addEventListener('click', function () {
        showAllCoverImages();
        document.getElementById('uploadedCoverImage').src = "";
        document.getElementById('uploadedCoverImage').style.display = 'none';
        document.getElementById('uploadCoverInput').value = null;


    });
    function displayMessageInChangeCoverImage(response) {
        console.log("---------------displayMessageInChangeCoverImage----------")
        $('#profileImageModal').modal('hide');
        console.log(response)
        var alert = document.getElementById("alert2");
        if (response.success == true && response.code == "succeeded") {

            var msg = `
                   <div class="alert alert-success alert-dismissible fade show">
                            <strong>Success!</strong>${response.message}.
                        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                   </div>
              `;
            alert.innerHTML = msg;
        }
        else if (response.success == false && response.code == "failed") {

            var msg = `
                       <div class="alert alert-danger alert-dismissible fade show">
                                 <strong>Error!</strong>${response.message}.
                             <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        </div>
                        `;
            alert.innerHTML = msg;

            showAllCoverImages
        }
    }
    function showAllCoverImages() {
        document.querySelector('.default-cover-images').innerHTML = `
        <div class="col-12 mb-3">
                <img src="/images/defaultimages/cover1.jpg" class="img-fluid rounded default-cover-image" alt="Default Image 1">
            </div>
            <div class="col-12 mb-3">
                <img src="/images/defaultimages/cover2.jpg" class="img-fluid rounded default-cover-image" alt="Default Image 1">
            </div>
            <div class="col-12 mb-3">
                <img src="/images/defaultimages/cover3.jpg" class="img-fluid rounded default-cover-image" alt="Default Image 1">
            </div>

    `;
        attachDefaultCoverImageListeners(); // Reattach event listeners after showing all images
    }

    function attachDefaultCoverImageListeners() {
        var defaultImages = document.querySelectorAll('.default-cover-images .default-cover-image');
        defaultImages.forEach(function (image) {
            image.addEventListener('click', function () {
                var defaultImageSrc = this.getAttribute('src');
                uploadDefaultCoverImage(defaultImageSrc);
            });
        });
    }
});