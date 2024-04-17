document.addEventListener('DOMContentLoaded', function () {
    var isDefault = false;
    var defaultImageSrc; // Define defaultImageSrc in a broader scope

    function uploadDefaultImage(imageSrc) {
        document.querySelector('.default-images').innerHTML = '';
        var imgDiv = document.createElement('div');
        imgDiv.className = 'col-md-4 mb-3';
        var img = document.createElement('img');
        img.src = imageSrc;
        img.className = 'img-fluid rounded default-image';
        img.alt = 'Selected Image';
        imgDiv.appendChild(img);
        document.querySelector('.default-images').appendChild(imgDiv);
        attachDefaultImageListeners(); // Reattach event listeners after adding default images
        defaultImageSrc = imageSrc; // Set defaultImageSrc when a default image is selected
        isDefault = true;
    }

    // Attach event listeners to default images
    function attachDefaultImageListeners() {
        var defaultImages = document.querySelectorAll('.default-images .default-image');
        defaultImages.forEach(function (image) {
            image.addEventListener('click', function () {
                var defaultImageSrc = this.getAttribute('src');
                uploadDefaultImage(defaultImageSrc);
            });
        });
    }

    attachDefaultImageListeners(); // Attach event listeners initially

    // Upload button click event handler
    document.getElementById('uploadBtn').addEventListener('click', function () {
        var formData = new FormData();

        if (!isDefault) {
            var fileInput = document.getElementById('uploadInput');
            var file = fileInput.files[0];
            formData.append('image', file);
        } else {
            formData.append('defaultImageSrc', defaultImageSrc);
        }

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/EditProfile/ChangePersonalImage');
        xhr.onload = function () {
            if (xhr.status === 200) {
                var responseData = JSON.parse(xhr.responseText);
                displayMessageInChangePersonalImage(responseData)
            } else {
                console.error(xhr.responseText);
            }
        };
        xhr.send(formData);
    });
    // Change event handler for file input
    document.getElementById('changeImage').addEventListener('click', function (event) {
        document.getElementById('uploadInput').addEventListener('change', function (event) {
            var file = event.target.files[0];
            var reader = new FileReader();

            reader.onload = function (event) {
                var imageUrl = event.target.result;
                document.querySelector('.default-images').innerHTML = '';
                document.getElementById('uploadedImage').src = imageUrl;
                document.getElementById('uploadedImage').style.display = 'block';
            };
            reader.readAsDataURL(file);
        });
    });

    // Click event handler for "Show All Images" button
    document.getElementById('showAllBtn').addEventListener('click', function () {
        showAllImages();
        document.getElementById('uploadedImage').src = "";
        document.getElementById('uploadedImage').style.display = 'none';
        document.getElementById('uploadInput').value = null;


    });
    function displayMessageInChangePersonalImage(response) {
        console.log("---------------displayMessageInChangePersonalImage----------")
        $('#imageModal').modal('hide');
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

            showAllImages
        }
    }
    function showAllImages() {
        document.querySelector('.default-images').innerHTML = `
         <div class="col-md-4 mb-3">
                <img src="~/images/defaultimages/women-avater.png" class="img-fluid rounded default-image" alt="Default Image 1">
            </div>
            <div class="col-md-4 mb-3">
                <img src="~/images/defaultimages/man-avatar.png" class="img-fluid rounded default-image" alt="Default Image 2">
            </div>
            <div class="col-md-4 mb-3">
                <img src="~/images/defaultimages/1.jpg" class="img-fluid rounded default-image" alt="Default Image 3">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/2.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/3.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
             <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/4.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/5.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/6.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/7.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/8.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/9.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/10.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/11.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/12.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/13.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/14.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/15.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/16.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/17.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/18.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>
            <div class="col-md-4 mb-3">
                <img src="/images/defaultimages/19.jpg" class="img-fluid rounded default-image" alt="Default Image 4">
            </div>

    `;
        attachDefaultImageListeners(); // Reattach event listeners after showing all images
    }

    function attachDefaultImageListeners() {
        var defaultImages = document.querySelectorAll('.default-images .default-image');
        defaultImages.forEach(function (image) {
            image.addEventListener('click', function () {
                var defaultImageSrc = this.getAttribute('src');
                uploadDefaultImage(defaultImageSrc);
            });
        });
    }
});


