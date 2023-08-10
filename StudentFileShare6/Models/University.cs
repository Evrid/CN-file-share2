using StudentFileShare6.data;
using StudentFileShare6.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("universities")]     //we must use it because it is related to table universities in SQL
public class University
{
    [Key]
    public string SchoolID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public List<Course>? Courses { get; set; }  //list of courses offered by a school
    public List<Document>? Documents { get; set; }   //list of documents offered by a school
    //By adding the ? after the list type (List<Course>? and List<Document>?), we indicate that these properties can be nullable, thus satisfying the validation requirements.

    public void GenerateRandomSchoolID(UniversityContext context)
    {
        // Generate a random unique SchoolID based on the Name and Location
        // You can implement your own logic here to ensure uniqueness
        // Example: concatenate a random number with the first letters of Name and Location

        var random = new Random();
        string generatedSchoolID;

        do
        {
            generatedSchoolID = $"{random.Next(1000, 9999)}{(Name.ToLower()).Substring(0, 2)}{(Location.ToLower()).Substring(0, 2)}";
            //This line generates the school ID by concatenating a random 4-digit number (random.Next(1000, 9999)) with the first two letters of the name (Name.Substring(0, 2)) and the first two letters of the location (Location.Substring(0, 2)).
        } while (!IsSchoolIDUnique(context, generatedSchoolID));
        //It will keep generating new IDs until a unique one is found, with IsSchoolIDUnique() function
        SchoolID = generatedSchoolID;
    }

    private bool IsSchoolIDUnique(UniversityContext context, string schoolID)
    {
        // Implement your logic to check if the generated SchoolID is unique
        // Example: query the database to see if any other University has the same SchoolID

        return !context.Universities.Any(u => u.SchoolID == schoolID);
        //The expression u => u is a lambda expression 
    }
}


