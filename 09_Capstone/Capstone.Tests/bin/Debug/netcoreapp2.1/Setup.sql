-- Start a transaction
--BEGIN Transaction

-- Remove all the data
DELETE FROM reservation;
DELETE FROM site;
DELETE FROM campground;
DELETE FROM park;

-- Insert into the park table
-- Acadia
INSERT INTO park (name, location, establish_date, area, visitors, description)
VALUES ('Acadia', 'Maine', '1919-02-26', 47389, 2563129, 'Covering most of Mount Desert Island and other coastal islands, Acadia features the tallest mountain on the Atlantic coast of the United States, granite peaks, ocean shoreline, woodlands, and lakes. There are freshwater, estuary, forest, and intertidal habitats.');
DECLARE @acadia INT
SELECT @acadia = @@IDENTITY;

-- Arches
INSERT INTO park (name, location, establish_date, area, visitors, description)
VALUES ('Arches',	'Utah', '1929-04-12', 76518,	1284767, 'This site features more than 2,000 natural sandstone arches, including the famous Delicate Arch. In a desert climate, millions of years of erosion have led to these structures, and the arid ground has life-sustaining soil crust and potholes, which serve as natural water-collecting basins. Other geologic formations are stone columns, spires, fins, and towers.');
DECLARE @arches INT
SELECT @arches = @@IDENTITY;

-- Cuyahoga
INSERT INTO park (name, location, establish_date, area, visitors, description)
VALUES ('Cuyahoga Valley', 'Ohio', '2000-10-11',32860,	2189849, 'This park along the Cuyahoga River has waterfalls, hills, trails, and exhibits on early rural living. The Ohio and Erie Canal Towpath Trail follows the Ohio and Erie Canal, where mules towed canal boats. The park has numerous historic homes, bridges, and structures, and also offers a scenic train ride.');
DECLARE @cuyVally INT
SELECT @cuyVally = @@IDENTITY;

-- Acadia Campground
INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@acadia, 'Blackwoods', 1, 12, 35.00);
DECLARE @blackwoods INT
SELECT @blackwoods = @@IDENTITY;

-- Arches Campground
INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@arches, 'Devil''s Garden', 1, 12, 25.00);
DECLARE @devil INT
SELECT @devil = @@IDENTITY;

-- CVNP Campground
INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@cuyVally, 'The Unnamed Primitive Campsites', 5, 11, 20.00);
DECLARE @unnamed INT
SELECT @unnamed = @@IDENTITY;

-- Blackwood sites
INSERT INTO site (site_number, campground_id, utilities) VALUES (4, @blackwoods, 1);
INSERT INTO site (site_number, campground_id, max_rv_length, accessible, utilities) VALUES (9, @blackwoods, 20, 1, 1);
INSERT INTO site (site_number, campground_id, max_rv_length, accessible, utilities) VALUES (12, @blackwoods, 35, 1, 1)
DECLARE @site1 INT
SELECT @site1 = @@IDENTITY;

-- Devil's Garden Sites
INSERT INTO site (site_number, campground_id, max_occupancy, accessible, utilities) VALUES (5, @devil, 10, 1, 1);
INSERT INTO site (site_number, campground_id, max_occupancy, max_rv_length, utilities) VALUES (7, @devil, 7, 20, 1);
INSERT INTO site (site_number, campground_id, max_occupancy, max_rv_length, utilities) VALUES (8, @devil, 7, 20, 1);
DECLARE @site2 INT
SELECT @site2 = @@IDENTITY;

-- Unanmed Sites
INSERT INTO site (site_number, campground_id) VALUES (1, @unnamed);
INSERT INTO site (site_number, campground_id) VALUES (3, @unnamed);
DECLARE @site3 INT
SELECT @site3 = @@IDENTITY;

-- Site 1 Reservation
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@site1, 'Jones Reservation', GETDATE()-2, GETDATE()+2);
DECLARE @res1 INT
SELECT @res1 = @@IDENTITY;

-- Site 2 REservations
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@site2, 'Beth Mooney', GETDATE(), GETDATE()+4);
DECLARE @res2 INT
SELECT @res2 = @@IDENTITY;

-- Site 3 Reservation
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@site3, 'Scott Family', GETDATE()+35, GETDATE()+40);
DECLARE @res3 INT
SELECT @res3 = @@IDENTITY;

-- SELECT to test
--SELECT * FROM park
--SELECT * FROM park ORDER BY name ASC
--SELECT * FROM campground WHERE park_id = @acadia
-- SELECT * FROM SITE WHERE campground_id = @blackwoods
--SELECT * FROM SITE WHERE campground_id = @blackwoods
--SELECT * FROM park WHERE park_id = @acadia;
-- Return some data to test
SELECT @acadia AS Acadia, @arches AS Arches, @cuyVally AS CuyVally,
@blackwoods AS Blackwoods, @devil AS Devil, @site1 AS Site1, @site2 AS Site2, @site3 AS Site3,
@res1 AS Res1, @res2 AS Res2, @res3 AS Res3;

--ROLLBACK TRANSACTION
-- Rollback Tran