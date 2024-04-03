'use strict'

document.addEventListener('DOMContentLoaded', function () {

    // Drop Down menu
    const logout = document.querySelector(".logout")

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
            console.log("Clicked")
            event.stopPropagation();
            profileBox.classList.toggle('active');
        });

        // Close the dropdown menu when clicking outside 
        document.addEventListener('click', function () {
            console.log("Clicked")
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

}