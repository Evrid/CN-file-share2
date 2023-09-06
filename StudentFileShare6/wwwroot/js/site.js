// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function debounce(func, delay) {     //When you listen to the 'input' event, multiple requests might be sent in quick succession. Debouncing can help reduce the number of requests.
    let debounceTimer;
    return function () {
        const context = this;
        const args = arguments;
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => func.apply(context, args), delay);
    };
}



function getUniversities(searchText) {
    fetch(`/Document/GetUniversities?searchText=${searchText}`)
        .then(response => response.json())
        .then(data => {
            customDatalist.innerHTML = '';
            customDatalist.style.display = 'block';

            if (Object.keys(data).length === 0) {
                // Show 'No matching university' if no universities are found
                const divElement = document.createElement('div');
                divElement.textContent = '没有匹配的大学';
                divElement.classList.add('custom-option');
                customDatalist.appendChild(divElement);
            } else {
                // Populate the dropdown with universities
                for (const key in data) {
                    if (data.hasOwnProperty(key)) {
                        const university = data[key];
                        const divElement = document.createElement('div');
                        divElement.textContent = university.name + " (" + university.location + ")";
                        divElement.classList.add('custom-option');
                        divElement.setAttribute('data-value', university.schoolID);
                        customDatalist.appendChild(divElement);
                    }
                }

                // Add click event listeners to each custom option
                document.querySelectorAll('.custom-option').forEach(option => {
                    option.addEventListener('click', function () {
                        const selectedValue = this.getAttribute('data-value');
                        inputElement.value = this.textContent;
                        hiddenInput.value = selectedValue;
                        customDatalist.style.display = 'none';
                    });
                });
            }
        });
}


function getCourses(searchText2) {
    fetch(`/Document/GetCourses?searchText=${searchText2}&SelectedSchoolID=${SelectedSchoolID}`)
        .then(response => response.json())
        .then(data => {
            customDatalist2.innerHTML = ''; // Clear the custom datalist
            customDatalist2.style.display = 'block'; // Display the custom datalist

            if (Object.keys(data).length === 0) {
                // Show 'No matching courses' if no courses are found
                const divElement = document.createElement('div');
                divElement.textContent = 'No matching courses';
                divElement.classList.add('custom-option');
                customDatalist2.appendChild(divElement);
            } else {
                // Populate the dropdown with courses
                for (const key in data) {
                    if (data.hasOwnProperty(key)) {
                        const course = data[key];
                        const divElement = document.createElement('div');
                        divElement.textContent = course.name;
                        divElement.classList.add('custom-option');
                        divElement.setAttribute('data-value', course.courseID);
                        customDatalist2.appendChild(divElement);
                    }
                }

                // Add click event listeners to each custom option
                document.querySelectorAll('.custom-option').forEach(option => {
                    option.addEventListener('click', function () {
                        const selectedValue = this.getAttribute('data-value');
                        courseInput.value = this.textContent;
                        hiddenInput2.value = selectedValue;
                        customDatalist2.style.display = 'none';
                    });
                });
            }
        });
}
