document.addEventListener("DOMContentLoaded", function (event) {

    const email = document.getElementById('email');

    const emailhid = document.getElementById('emailhid');
    emailhid.innerHTML = hideEmail(email.innerHTML);
    function hideEmail(email) {
        const atIndex = email.indexOf('@');
        const username = email.substring(0, atIndex); 
        const visibleChars = Math.min(2, username.length);
        const hiddenUsername = '*'.repeat(username.length - visibleChars) + username.substring(username.length - visibleChars); // Replace all characters in the username except for the last two characters
        const domain = email.substring(atIndex); 
        return hiddenUsername + domain; 
    }
    function OTPInput() {
        const inputs = document.querySelectorAll('#otp > *[id]');
        for (let i = 0; i < inputs.length; i++) {
            inputs[i].addEventListener('keydown', function (event) {
                if (event.key === "Backspace") {
                    inputs[i].value = '';
                    if (i !== 0) inputs[i - 1].focus();
                } else {
                    if (i === inputs.length - 1 && inputs[i].value !== '') {
                        return true;
                    } else if (event.keyCode > 47 && event.keyCode < 58) {
                        inputs[i].value = event.key;
                        if (i !== inputs.length - 1) inputs[i + 1].focus();
                        event.preventDefault();
                    } else if (event.keyCode > 64 && event.keyCode < 91) {
                        inputs[i].value = String.fromCharCode(event.keyCode);
                        if (i !== inputs.length - 1) inputs[i + 1].focus();
                        event.preventDefault();
                    }
                }
            });
        }
    }

    OTPInput();

    // Add event listener to the Validate button
    document.getElementById("validateBtn").addEventListener("click", function () {
        // Check if all inputs have data
        const inputs = document.querySelectorAll('#otp > *[id]');
        for (let i = 0; i < inputs.length; i++) {
            if (inputs[i].value.trim() === '') {
                alert('Please fill in all the OTP fields.');
                return; // Exit function if any input is empty
            }
        }

        const otpData = Array.from(inputs).map(input => input.value).join('');
        console.log(otpData)
        $.ajax({
            url: '/Account/confirmEmail',
            method: 'POST',
            data: { code: otpData },
            success: function (data, textStatus, jqXHR) {
                if (jqXHR.status === 200) {
                    window.location.href = "/EditProfile/CompleteProfile";
                } else {
                    displayMessage(response);
                }
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error(error);
            }
        });
    });
    function displayMessage(response) {

        var msg = `
                       <div class="alert alert-danger alert-dismissible fade show">
                                 <strong>Error! </strong>${response.message}.
                        </div>
                        `;
       
        const alert = document.getElementById('alert');
        alert.innerHTML = msg;

        //setTimeout(function () {
        //    const alert = document.getElementById('alert');
        //    alert.style.display = 'none';
        //}, 2000);
    }

});