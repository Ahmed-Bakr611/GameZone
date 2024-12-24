// List of allowed file extensions and the maximum file size (1MB)
const allowedExtensions = ['jpg', 'jpeg', 'png'];  // Replace with your allowed extensions
const maxSize = 1 * 1024 * 1024;  // 1MB in bytes

// Function to validate the file
function validateFile(file) {
    const errorMessage = $('#file-error');

    // Clear previous error message
    errorMessage.text('');

    // If no file is selected
    if (!file) {
        errorMessage.text('File is required.');
        return false;
    }

    // Check file extension
    const fileExtension = file.name.split('.').pop().toLowerCase();
    if (!allowedExtensions.includes(fileExtension)) {
        errorMessage.text('Invalid file extension. Allowed extensions are: ' + allowedExtensions.join(', ') + '.');
        return false;
    }

    // Check file size
    if (file.size > maxSize) {
        errorMessage.text('File size exceeds the 1MB limit.');
        return false;
    }

    // If all checks pass, return true
    return true;
}

 //File input change event
 $('#CoverUrl').on('change', function () {
     const file = this.files[0];  Get the selected file
     validateFile(file);  Validate file and show error message if invalid
 });

$(document).on('click', function (event) {
    // Check if the clicked element is not the file input field or its children
    if (!$(event.target).closest('#CoverUrl').length) {
        const file = $('#CoverUrl')[0].files[0];
        validateFile(file);  // Ensure the error message stays if the file is invalid
    }
});
