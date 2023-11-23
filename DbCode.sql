
    DROP TABLE IF EXISTS Measurment;

    DROP TABLE IF EXISTS MQTT_Message_Unit;

    DROP TABLE IF EXISTS Optimal_Value;

    DROP TABLE IF EXISTS Category;

    DROP TABLE IF EXISTS Measured_Unit;

    DROP TABLE IF EXISTS Favorite_Station;

    DROP TABLE IF EXISTS Coordinates;

    DROP TABLE IF EXISTS Stations;

    DROP TABLE IF EXISTS MQTT_Server;

CREATE TYPE status AS ENUM ('enabled', 'disable');


CREATE TABLE MQTT_Server (
    ID_Server CHAR(3) PRIMARY KEY,
    Url VARCHAR(100),
    Status status
);

CREATE TABLE Stations (
    City VARCHAR(50),
    Name_Station VARCHAR(250),
    ID_Station CHAR(4) PRIMARY KEY,
    Status status,
    ID_Server CHAR(3),
    ID_SaveEcoBot VARCHAR(50),
    Coordinates POINT,
     FOREIGN KEY (ID_Server) REFERENCES MQTT_Server(ID_Server)
);

/*CREATE TABLE Coordinates (
    ID_Station CHAR(4) PRIMARY KEY,
    Longitude FLOAT,
    Latitude FLOAT,
   FOREIGN KEY (ID_Station) REFERENCES Stations(ID_Station)
);*/

CREATE TABLE Favorite_Station (
    User_Name CHAR(25),
    ID_Station CHAR(4),
    PRIMARY KEY(User_Name, ID_Station),
    FOREIGN KEY (ID_Station) REFERENCES Stations(ID_Station)

);


CREATE TABLE Measured_Unit (
    Title VARCHAR(50),
    Unit VARCHAR(50),
    ID_Measured_Unit CHAR(4) PRIMARY KEY
);

CREATE TABLE Category (
    ID_Category CHAR(3) PRIMARY KEY,
    Designation VARCHAR(250)
);

CREATE TABLE Optimal_Value (
    Bottom_Border FLOAT,
    Upper_Border FLOAT,
    ID_Category CHAR(3),
    ID_Measured_Unit CHAR(4),
    PRIMARY KEY  (ID_Category, ID_Measured_Unit),
   FOREIGN KEY (ID_Category) REFERENCES Category(ID_Category),
    FOREIGN KEY (ID_Measured_Unit) REFERENCES Measured_Unit(ID_Measured_Unit)
);


CREATE TABLE MQTT_Message_Unit (
    ID_Station CHAR(4),
    ID_Measured_Unit CHAR(4),
    Message_ VARCHAR(250),
    Queue_Number INT,
    PRIMARY KEY (ID_Station, ID_Measured_Unit),
    FOREIGN KEY (ID_Station) REFERENCES Stations(ID_Station),
    FOREIGN KEY (ID_Measured_Unit) REFERENCES Measured_Unit(ID_Measured_Unit)

);


Create Table Measurment(
  Date_Time TIMESTAMP,
  Value_ FLOAT,
  ID_Measurment CHAR(5) PRIMARY KEY,
  ID_Station CHAR(4),
  ID_Measured_Unit CHAR(4),
  FOREIGN KEY (ID_Station) REFERENCES Stations(ID_Station),
  FOREIGN KEY (ID_Measured_Unit) REFERENCES Measured_Unit(ID_Measured_Unit)

);







-- views --




--1
Create view Connected_Stations_Info AS
SELECT distinct
    s.Name_Station AS Station_Name,
    s.City,    
    s.Status AS Station_Status,
    min_connection_date AS Connection_Date,
    mu.Title AS Measured_Unit_Title,
    mu.Unit AS Measured_Unit_Unit
FROM
    Stations AS s
INNER JOIN
    Measurment AS m ON s.ID_Station = m.ID_Station
INNER JOIN
    Measured_Unit AS mu ON m.ID_Measured_Unit = mu.ID_Measured_Unit
INNER JOIN (
    SELECT
        ID_Station,
        MIN(Date_Time) AS min_connection_date
    FROM
        Measurment
    GROUP BY
        ID_Station
) AS min_dates ON s.ID_Station = min_dates.ID_Station
ORDER BY
   s.Name_Station;

--2
Create view measurement_report AS 
SELECT
    MU.Title AS Measured_Unit_Title,
    MAX(M.Value_) AS Max_Value,
    MIN(M.Value_) AS Min_Value,
    CAST(AVG(M.Value_) AS NUMERIC(10, 3)) AS Avg_Value,
    MU.Unit AS Measured_Unit_Unit
FROM
    Measurment M
INNER JOIN
    Measured_Unit MU ON M.ID_Measured_Unit = MU.ID_Measured_Unit
WHERE
    M.ID_Station = '0005'
    AND M.Date_Time >=  '17.05.2021 22:10:00'
    AND M.Date_Time <= '17.12.2023 22:10:00'
GROUP BY
    M.ID_Station,
    M.ID_Measured_Unit,
    MU.Title,
    MU.Unit;

--3
Create view MaxMeasurementView AS
SELECT
    Stations.City,
    Measured_Unit.Title AS Unit_Title,
    MAX(Measurment.Value_) AS Max_Value
FROM
    Measurment
JOIN Stations ON Measurment.ID_Station = Stations.ID_Station
JOIN Measured_Unit ON Measurment.ID_Measured_Unit = Measured_Unit.ID_Measured_Unit
WHERE
    Measured_Unit.Title IN ('PM2.5', 'PM10') -- Вибираємо тільки PM2.5 та PM10
GROUP BY
    Stations.City, Measured_Unit.Title;



--4
Create view V_7L as
SELECT C.Designation AS "Назва_категорії",
       COUNT(*) AS "Кількість_перевищень"
FROM Measurment M
INNER JOIN Optimal_Value OV ON M.ID_Measured_Unit = OV.ID_Measured_Unit
INNER JOIN Category C ON OV.ID_Category = C.ID_Category
INNER JOIN Measured_Unit U ON M.ID_Measured_Unit = U.ID_Measured_Unit
WHERE  
      U.Title = 'PM2.5'
  AND M.Date_Time BETWEEN DATE_TRUNC('day', M.Date_Time) AND DATE_TRUNC('day', M.Date_Time) + INTERVAL '1 day'
  AND M.Value_ > OV.Bottom_Border
  AND C.ID_Category IN ('4', '5', '6')
GROUP BY C.Designation;



--5
Create View Sulfur_V as(
SELECT c.Designation AS Category,
       COUNT(*) AS Measurement_Count
FROM Measurment m
INNER JOIN Optimal_Value ov ON m.ID_Measured_Unit = ov.ID_Measured_Unit
INNER JOIN Category c ON ov.ID_Category = c.ID_Category
WHERE 
      (Value_<ov.Upper_Border And Value_>ov.Bottom_Border  )
      AND m.ID_Measured_Unit = '13'
GROUP BY c.Designation
    )

--6
Create View carbon_v as(
SELECT c.Designation AS Category,
       COUNT(*) AS Measurement_Count
FROM Measurment m
INNER JOIN Optimal_Value ov ON m.ID_Measured_Unit = ov.ID_Measured_Unit
INNER JOIN Category c ON ov.ID_Category = c.ID_Category
WHERE 
      (Value_<ov.Upper_Border And Value_>ov.Bottom_Border  )
      AND m.ID_Measured_Unit = '12'
GROUP BY c.Designation;
    )


--   Data for DB  ---   
----- https://drive.google.com/drive/folders/1is7u1Ou-uk5iaOLXflmmknDv7Ez9WaW_
