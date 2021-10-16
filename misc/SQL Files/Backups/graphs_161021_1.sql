-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 12, 2021 at 11:47 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.2.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `graphs`
--

-- --------------------------------------------------------

--
-- Table structure for table `presets`
--

CREATE TABLE `presets` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `serial` varchar(10000) DEFAULT NULL,
  `img_dir` varchar(511) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `presets`
--

INSERT INTO `presets` (`id`, `name`, `serial`, `img_dir`) VALUES
(1, 'Undirected connected', '0: (1,12), \n1: (0,12), (2,14), (3,13), (5,13), \n2: (1,14), (4,11), (5,6), \n3: (1,13), (4,5), (5,2), (6,7), \n4: (2,11), (3,5), (5,3), \n5: (1,13), (2,6), (3,2), (4,3), \n6: (3,7), \n', '\\Forms\\FDGV\\PresetImgs\\1.png'),
(2, 'Undirected connected', '0: (1,2), (5,2), (6,13), \n1: (0,2), (2,11), (6,2), \n2: (1,11), (3,3), (6,10), \n3: (2,3), (4,7), (6,6), \n4: (3,7), (5,2), (6,6), \n5: (4,2), (0,2), (6,14), \n6: (0,13), (1,2), (2,10), (3,6), (4,6), (5,14), \n', '\\Forms\\FDGV\\PresetImgs\\2.png'),
(3, 'Undirected disconnected', '0: (1,4), \n1: (0,4), (2,2), \n2: (1,2), \n3: (4,8), \n4: (3,8), (5,6), \n5: (4,6), (6,7), \n6: (5,7), \n7: (8,6), \n8: (7,6), \n9: (10,9), (11,3), (12,5), \n10: (9,9), \n11: (9,3), \n12: (9,5), \n', '\\Forms\\FDGV\\PresetImgs\\3.png'),
(4, 'Undirected disconnected', '0: (2,12), (3,14), \n1: (4,2), \n2: (0,12), (3,5), \n3: (0,14), (2,5), \n4: (1,2), \n5: (6,4), \n6: (5,4), (7,1), \n7: (6,1), \n8: \n', '\\Forms\\FDGV\\PresetImgs\\4.png'),
(5, 'DAG', '0: (4,11), (6,5), \n1: (2,3), \n2: (3,12), (4,4), \n3: \n4: (5,5), (7,1), \n5: \n6: (4,6), \n7: \n', '\\Forms\\FDGV\\PresetImgs\\5.png'),
(6, 'DAG', '0: (2,10), \n1: (2,8), (5,9), \n2: \n3: (2,11), \n4: (5,1), \n5: (8,6), \n6: (4,8), \n7: (5,13), \n8: (10,11), \n9: (8,9), \n10: \n11: (12,1), \n12: \n', '\\Forms\\FDGV\\PresetImgs\\6.png'),
(7, 'Ternary tree', '0: (1,10), (2,13), \n1: (3,9), (4,9), (5,2), \n2: (6,7), (7,4), \n3: (8,9), (9,5), \n4: \n5: \n6: (10,4), (11,8), (12,3), \n7: \n8: \n9: \n10: \n11: \n12: \n', '\\Forms\\FDGV\\PresetImgs\\7.png'),
(8, 'K5 - Undirected connected', '0: (1,13), (2,7), (3,8), (4,13), \n1: (0,13), (2,3), (3,5), (4,14), \n2: (0,7), (1,3), (3,4), (4,7), \n3: (0,8), (1,5), (2,4), (4,3), \n4: (0,13), (1,14), (2,7), (3,3), \n', '\\Forms\\FDGV\\PresetImgs\\8.png'),
(19, 'SCC tests', '0: (1,1), \n1: (2,1), \n2: (0,1), \n3: (4,1), (2,1), \n4: (5,1), \n5: (3,1), \n', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `presets`
--
ALTER TABLE `presets`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `presets`
--
ALTER TABLE `presets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
