// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function getUniversities(searchText) {
    fetch(`/Document/GetUniversities?searchText=${searchText}`)
        //the GetUniversities function in DocumentController.cs is not directly called in the Create.cshtml file.
        // Instead, it is called asynchronously from JavaScript code using the fetch API.
        .then(response => response.json())
        .then(data => {
            datalistElement.innerHTML = ''; // Clear the datalist
            //       console.log(data); // Log the response data to the console, works well
            //        console.log(typeof(data));
            //if the typeof(data) is showing "object" instead of an array, it suggests that the data variable is an object rather than an array.

       


            for (const key in data) {
                // iterate over the enumerable properties of an object.
                if (data.hasOwnProperty(key)) {
                    //The hasOwnProperty() method is used to ensure that the property belongs directly to the data object itself and not to its prototype chain.
                    const university = data[key];
                    console.log(university);  // works well
                    const optionElement = document.createElement('option');
                    optionElement.text = university.name + " (" + university.location + ")";
                    optionElement.setAttribute('data-value', university.schoolID);
                    //  console.log(university.name);   //works well
                    datalistElement.appendChild(optionElement);
                }
              
            }

            // Event listener to handle option selection, set the value of input “schoolID” to the corresponding “name” of the school user selected
            schoolInput.addEventListener('input', function (event) {
                const selectedText = event.target.value;
                const selectedOption = Array.from(datalistElement.options).find(option => option.text === selectedText);

                if (selectedOption) {
                    const selectedValue = selectedOption.getAttribute('data-value');
                    hiddenInput.value = selectedValue;
                    console.log('Selected UniversityID Value:', selectedValue);
                    SelectedSchoolID = hiddenInput.value;    //for select course only in that university
                    //    console.log(' SelectedSchoolID:', SelectedSchoolID);
                    console.log(typeof (SelectedSchoolID));
                } else {
                    hiddenInput.value = '';
                    console.log('Selected UniversityID Value: None');
                }
            });
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
