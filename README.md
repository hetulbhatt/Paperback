# Paperback
A book rental system

This is an online portal which provides subscription to eBooks fields at minimal cost. It is a .Net MVC project with OAuth authentication using Email. We have used Entity Framework with MSSQL database. We have deployed our web application on Azure Cloud Services.

User needs to select subject of the book which he/she is expecting. After selecting subject, all books related to that particular subject will be displayed to user. After successfully finding the book, user can subscribe to that book for duration in term of months. User can read any subscribed book from "My Book" section.

The tricky part was to restrict users from downloading the PDFs which was tackled by making a custom PDF viewer specially for this project using PDF.js.
