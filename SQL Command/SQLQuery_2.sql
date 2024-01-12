SET IDENTITY_INSERT Users ON;

INSERT INTO Users(Id, Name, Password, Email)
VALUES
(1, 'Jane', 'Jane123', 'janesmith@gmail.com'),
(2, 'Alice', 'Alice456', 'aliceinwonderland@gmail.com');

INSERT INTO Books(Id, Title, Author, Category, IsBorrowed, Path)
VALUES
(1, 'Cay cam ngot cua toi', 'Jose Mauro De Vasconcelos', 'novel' , 1, '/home/huylele/CODE/Lib2/BookContent/cay-cam-ngot-cua-toi.txt'),
(2, 'Tri tue do thai', 'Eran Katz', 'novel' , 0, '/home/huylele/CODE/Lib2/BookContent/tri-tue-do-thai.txt');

INSERT INTO Notifications(Id, Content, CreatedAt)
VALUES
(1, 'Ngày mai (29/11) thư viện đóng cửa', GETDATE()),
(2, 'Có sự kiện sale', GETDATE());

INSERT INTO Loans(Id, UserId, BookId, LoanDate, ReturnDate, DueDate)
VALUES
(1, 1, 2, GETDATE(), GETDATE()+30, GETDATE()+14),
(2, 2, 1, GETDATE(), GETDATE()+30, GETDATE()+14);