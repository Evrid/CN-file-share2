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
                        divElement.textContent = university.name;
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

        //the GetUniversities function in DocumentController.cs is not directly called in the Create.cshtml file.
        // Instead, it is called asynchronously from JavaScript code using the fetch API.
        .then(response => response.json())
        .then(data => {
            console.log("got json reponse");
            console.log(data);
            datalistElement2.innerHTML = ''; // Clear the datalist
            //       console.log(data); // Log the response data to the console, works well
            //        console.log(typeof(data));
            //if the typeof(data) is showing "object" instead of an array, it suggests that the data variable is an object rather than an array.
            for (const key in data) {
                console.log(key);
                // iterate over the enumerable properties of an object.
                if (data.hasOwnProperty(key)) {
                    //The hasOwnProperty() method is used to ensure that the property belongs directly to the data object itself and not to its prototype chain.
                    const course = data[key];
                    console.log(course);  // works well
                    const optionElement = document.createElement('option');
                    optionElement.text = course.name;
                    optionElement.setAttribute('data-value', course.courseID);
                    //   console.log(course.name);   //works well
                    datalistElement2.appendChild(optionElement);
                }
            }

            // Event listener to handle option selection, set the value of input “schoolID” to the corresponding “name” of the school user selected
            courseInput.addEventListener('input', function (event) {
                const selectedText = event.target.value;
                const selectedOption = Array.from(datalistElement2.options).find(option => option.text === selectedText);

                if (selectedOption) {
                    const selectedValue = selectedOption.getAttribute('data-value');
                    hiddenInput2.value = selectedValue;
                    console.log('Selected CourseID Value:', selectedValue);
                } else {
                    hiddenInput2.value = '';
                    console.log('Selected CourseID Value: None');
                }
            });
        });
}