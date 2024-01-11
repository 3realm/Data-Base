/*
CREATE DATABASE session_result;
*/
#-- Создание --
/*
CREATE TABLE Studednt (
    student_id INT AUTO_INCREMENT PRIMARY KEY,
    student_number VARCHAR(9) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    birth_date DATE NOT NULL
);

CREATE TABLE Teacher (
    teacher_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL
);

CREATE TABLE Subject (
    subject_id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    semester INT NOT NULL,
    teacher_id INT,
    FOREIGN KEY (teacher_id) REFERENCES Teacher(teacher_id)
);

CREATE TABLE Grades (
    grade_id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT,
    subject_id INT,
    grade INT,
    FOREIGN KEY (student_id) REFERENCES Studednt(student_id),
    FOREIGN KEY (subject_id) REFERENCES Subject(subject_id)
);
*/
##-- Заполнение --
/*

INSERT INTO Studednt (student_number, full_name, birth_date)
VALUES 
('26845', 'Милий Алексеевич Балакирев', '1837-02-12'),
('29470', 'Модест Петрович Мусоргский', '1829-03-21'),
('41297', 'Александр Порфирьевич Бородин', '1833-12-26'),
('41385', 'Николай Андреевич Римский-Корсаков', '1844-08-18'),
('75949', 'Цезарь Антонович Кюи', '1824-01-01');

INSERT INTO Teacher (full_name)
VALUES
('Николай Васильевич Арцыбушев'),
('Алекса́ндр Константи́нович Глазуно́в'),
('Серге́й Миха́йлович Ляпуно́в'),
('Васи́лий Серге́евич Кали́нников'),
('Никола́й Никола́евич Черепни́н');

INSERT INTO Subject (title, semester, teacher_id)
VALUES
('Music',1,1),
('Music',2,2),
('Music',3,2),
('Music',4,3),
('Philosophy',1,1),
('Philosophy',2,3),
('Philosophy',3,4),
('Philosophy',4,4);

INSERT INTO Grades (student_id, subject_id, grade)
VALUES 
(1,1,5),
(2,1,4),
(3,1,5),
(4,1,5),
(5,1,4),
(1,2,5),
(2,2,4),
(3,2,4),
(4,2,3),
(5,2,3),
(1,3,4),
(2,3,3),
(3,3,5),
(4,3,5),
(5,3,3),
(1,5,5),
(2,5,3),
(3,5,2),
(4,5,3),
(5,5,2);
*/
#-- UPDATE_&_DELETE
/*
insert into studednt (student_number, full_name, birth_date)
values
('12333', 'Иванов Владислав Денисович', '2003-03-29');

insert into studednt (student_number, full_name, birth_date)
values
('22222', 'Иванов Второй Сергеевич', '2003-03-29'),
('33333', 'Иванов Третий Сергеевич', '2003-03-29');

UPDATE Studednt SET full_name = 'Иванов Измененный Первичний' WHERE student_id = 8;

DELETE FROM Studednt WHERE student_id = 8;

SELECT * FROM studednt
*/
#-- 1 --
select * from subject order by title;
#-- 2 -- 
select count(*) from studednt;
#-- 3 --
SELECT * FROM studednt WHERE full_name LIKE 'Ива%';
#-- 4 --

SELECT S.full_name
FROM studednt S
JOIN Grades G ON S.student_id = G.student_id
WHERE G.grade = 5 AND G.subject_id = 1
ORDER BY S.full_name DESC;

#-- 5 --

SELECT S.full_name
FROM studednt Ss
INNER JOIN Grades G ON S.student_id = G.student_id
WHERE G.subject_id IN (1, 2, 5)
GROUP BY S.full_name
HAVING SUM(G.grade) > 12 AND COUNT(DISTINCT G.subject_id )=3;

select * from studednt;
select * from grades; 
select * from subject
