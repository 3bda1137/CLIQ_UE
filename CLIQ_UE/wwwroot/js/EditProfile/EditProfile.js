function GetDataOfUser() {
    $.ajax({
        type: 'POST',
        url: "/EditProfile/GetData",
        success: function (response) {

            console.log(response);
            displayData(response);
        },
        error: function (xhr, status, error) {

            console.error(xhr.responseText);
        }
    });
}
function getInputElements() {
    // Get input elements
    var firstName = document.getElementById('FirstName');
    var lastName = document.getElementById('LastName');
    var gender = document.getElementById('Gender');
    var language = document.getElementById('Language');
    var birthDate = document.querySelector('input[type="date"]'); 
    return {
        firstName: firstName,
        lastName: lastName,
        gender: gender,
        language: language,
        birthDate: birthDate
    };
}
function displayData(data) {
    console.log("----------start displayData--------------------");
    console.log(data);

    var inputs = getInputElements();
    inputs.firstName.value = data.firstName;
    inputs.lastName.value = data.lastName;
    inputs.gender.value = data.gender;
    inputs.language.value = data.language;
    inputs.birthDate.value = data.birthDate.split('T')[0];

    console.log("----------end displayData--------------------");
}

document.addEventListener('DOMContentLoaded', function () {
    // Add event listener to the first name input field
    var firstNameInput = document.getElementById('FirstName');
    firstNameInput.addEventListener('input', function () {
        // Hide the validation message
        document.getElementById('firstNameValidation').innerText = '';
    });

    // Add event listener to the last name input field
    var lastNameInput = document.getElementById('LastName');
    lastNameInput.addEventListener('input', function () {
        // Hide the validation message
        document.getElementById('lastNameValidation').innerText = '';
    });
});
function SendData() {
    var inputs = getInputElements();
    var firstName = inputs.firstName.value;
    var lastName = inputs.lastName.value;
    var gender = inputs.gender.value;
    var language = inputs.language.value;
    var birthDate = inputs.birthDate.value;

    // Validate first name
    if (firstName.trim() === '') {
        document.getElementById('firstNameValidation').innerText = 'First name cannot be empty';
        return;
    } else {
        document.getElementById('firstNameValidation').innerText = '';
    }

    // Validate last name
    if (lastName.trim() === '') {
        document.getElementById('lastNameValidation').innerText = 'Last name cannot be empty';
        return;
    } else {
        document.getElementById('lastNameValidation').innerText = '';
    }

    // Construct JSON object
    var newData = {
        "firstName": firstName,
        "lastName": lastName,
        "gender": gender,
        "language": language,
        "birthDate": birthDate
    };
    sendDataToAction(newData);
}

function sendDataToAction(data) {
    console.log("-----------start sendDataToAction---------------")
    console.log(data);
    $.ajax({
        type: 'POST',
        url: "/EditProfile/UpDataOfUser",
        data: data,
        success: function (response) {

            console.log(response);
            displayMessageInChangeinformation(response);
        },
        error: function (xhr, status, error) {

            console.error(xhr.responseText);
        }
    });
    console.log("-----------end sendDataToAction---------------")

}


function displayMessageInChangeinformation(response) {
    console.log("---------------displayMessageInChangeCoverImage----------")

    $('#exampleModalToggle3').modal('hide');
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




//----------------------------------


$(document).ready(function () {
    // Ensure the document is ready before binding events
    $('#changePasswordButton').on('click', function () {
        // Bind click event to the changePasswordButton
        var formData = {
            CurrentPassword: $('#CurrentPassword').val(),
            NewPassword: $('#NewPassword').val(),
            ConfirmNewPassword: $('#ConfirmNewPassword').val()
        };
        console.log("*******************************************")
        console.log(formData);

        // Send AJAX request to change password
        $.ajax({
            type: 'POST',
            url: "/EditProfile/ChangePassword",
            data: formData,
            success: function (response) {
                // Handle successful response
                displayMessage(response);
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error(xhr.responseText);
            }
        });
    });
});

function displayMessage(response) {
    console.log(response)
    // Display appropriate message based on response
    if (response.success == false && response.code == -1) {
        var currentPasswordValidationElement = document.getElementById("CurrentPasswordvalidation");
        currentPasswordValidationElement.innerHTML = response.message;
    } else if (response.success == true ) {
        var alert = document.getElementById("alert");
        var msg = `
            <div class="alert alert-success alert-dismissible fade show">
                <strong>Success!</strong>${response.message}.
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
        alert.innerHTML = msg;
        Clear(); // Clear input fields
    } else if (response.success == false ) {
        var alert = document.getElementById("alert");
        var msg = `
            <div class="alert alert-danger alert-dismissible fade show">
                <strong>Error!</strong>${response.message}.
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
        alert.innerHTML = msg;
         // Clear input fields
    }
    Clear();
}

$('#coseX').on('click', function () {
    Clear();
})
function Clear() {
    // Clear input fields
    $('#CurrentPassword').val('');
    $('#NewPassword').val('');
    $('#ConfirmNewPassword').val('');
}

