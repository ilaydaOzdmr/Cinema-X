Cinema-X README

Overview:

Cinema-X project is a database-driven dynamic website application designed for cinema owners to provide more convenient service to their customers. It consists of separate logins for admins and users, movie adding-removing and selection pages, ticket display, payment page and comment page.

Technologies Used:
Frontend: HTML, CSS, JavaScript
Backend: C#, ASP.NET Web Forms
Database: Microsoft SQL Server
Local Storage: For storing user preferences

General Features:
1)User Login and Registration: Users can log in and register.
2)Movie Search and View: Users can search by movie titles, descriptions, genres or actors and view movies by date.
3)Ticket Reservation: Users can select movies, sessions and seats. Movies are marked as "taken" if selected.
4)Payment: Users choices are priced and payment is taken by checking card information. 
5)Comment and Rating: Users can comment on movies and give ratings.
6)Admin Panel: Admin can add, update or delete movies. Also can manage reservations.

Features Details:

1)User Login and Registration::
1.a)User Login: If the user is registered, he/she accesses the user page by entering his/her email and password. If the user wants to log out, click the "Logout" button and exit.
1.b)User Registration: If the user is not registered, he/she goes to the registration page by clicking on the "Sign Up" link. He/she creates a registration by entering the requested information.

2)Movie Search and View:
2.a)Movie Search:Users can search by movie titles, descriptions, genres or actors. The system brings the movies that match the searches.
2.b)Movie Viewing: when you first enter both the system homepage and the user homepage, the movies that are on today's screens and their features are displayed. The user can change the date and create a reservation for a movie on a different date.

3)Ticket Reservation:
3.a)Movie Selection: The user selects the appropriate session from the movies he/she has viewed and is directed to select a seat.
3.b)Seat Selection: The user selects one or more unselected/empty seats and a tab opens for each seat. In these tabs, the user enters the name for the ticket, selects the ticket type and promotion.

4)Payment:The user views the ticket selections and enters the card information. If the card information is correct, the payment is completed and the user is directed to the ticket display page.

5)Comment and Rating: The user can comment and rate the movie. He/she can also view other user comments.

6)Admin Panel:
6.a)Upload And Menage Film Information: Admin can upload movies, update movie information and delete movies.
6.b)Menage Users: Admin can list user information.


Running the Project:
1)Open the project in Visual Studio.
2)Install the SQL Server database. Run the necessary SQL scripts to create the database structure.
3)Match the connection string in the Web.config file to your SQL Server instance.
4)Run the application locally.
5)When the project runs, the "Homepage" will open. You can perform the desired action by clicking according to the instructions in the relevant sections.


Database Details:

Tables:

1)Admins: Stores admin information.

2)CardInfos: Stores card information for payment.

3)DeletedMovies: Stores movie information deleted by admin.

4)MovieComment: Saves details for movie reviews.

5)Movies: Stores added movie information.

6)PricesForTicket: Stores pricing information for ticket types and promotions.

7)Reservations: Stores the selected and paid seats and the movie session they belong to.

8)Users: Stores registered user information.


Views:

1)vw_ShowMembers: Allows the Admin to see the necessary information about the user. (Admin should not be able to see all the information.)


Functions:

1)Check_Card_Infos: Checks the card information and returns 1 if correct and 0 if incorrect.

2)GetAverageRating: It takes the ratings given by users for movies and returns the average rating.

3)ShowRemainingDays: Gets the start and end dates of movies and returns their remaining running time.


Stored Procedures:

1)AdminLogin: Checks the admin login information (Admins table). Returns 1 if true, 0 if false.

2)GetMoviesByDate: Gets "ReleaseDate" and "EndDate" from the "Movies" table. Returns movies between these dates based on the selected date.

3)RegisterUser: When the user registers, it gets the information and saves it in the "Users" table.

4)SearchMovies: It searches the features of the movies according to the words searched by the user and returns what it finds.

5)UserLogin: Checks the user information (Users table) and returns 1 if correct and 0 if incorrect.


Triggers:

1)trg_DeleteMovieNoShowtimes(on Movies): If the admin does not enter any showtime while adding the movie, it will be triggered and immediately delete the movie record.

2)trg_InsertDeletedMovie(on Movies): Triggered when admin deletes a movie and writes the deleted movie's items to the "DeletedMovies" table.

3)trg_DeleteEmptyReviews(on MovieComment): When the user comments, if there is no movie name or comment, it is triggered and the comment is deleted immediately.

