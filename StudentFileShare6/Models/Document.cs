using Microsoft.AspNetCore.Identity;
using StudentFileShare6.Models;
using StudentFileShare6.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFileShare6.Models
{
    public class Document
    {
        public string DocumentID { get; set; }
        public string Name { get; set; }
        public string SchoolID { get; set; }
        public string CourseID { get; set; }
        public int Year { get; set; }
        public int Category { get; set; }
        public string? Description { get; set; }
        public float? Rating { get; set; }
        //we need to change SQL's Rating manually from Real to Float using ALTER TABLE Document ALTER COLUMN Rating FLOAT;
        //otherwise we will get error
        //   SELECT DATA_TYPE
        //FROM INFORMATION_SCHEMA.COLUMNS
        //WHERE TABLE_NAME = 'Document'
        //  AND COLUMN_NAME = 'Rating';

        public bool Anonymous { get; set; }
        public string UserID { get; set; }
        public string? Link { get; set; }   //link to download the document
                                            //   public string? UniversitiesSchoolID { get; set; }
        public int? LikeNumber { get; set; }

        public int? DislikeNumber { get; set; }
        public string? FirstPageImageLink { get; set; }

        public University? University { get; set; }
        public Course? Course { get; set; }
        [NotMapped]
        public List<FileUploadProgress>? FileUploadProgresses { get; set; }   //to show upload percentage


        public void GenerateRandomDocumentID(DocumentContext context)
        {
            var random = new Random();
            string generatedDocumentID;

            do
            {
                generatedDocumentID = $"{random.Next(10000, 99999)}{SchoolID.Substring(0, 2)}{CourseID.Substring(0, 2)}";
            } while (!IsDocumentIDUnique(context, generatedDocumentID));

            DocumentID = generatedDocumentID;
        }





        private bool IsDocumentIDUnique(DocumentContext context, string documentID)
        {
            return !context.Document.Any(d => d.DocumentID == documentID);
             
        }
    }
}






