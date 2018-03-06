-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Mar 06, 2018 at 01:55 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dungeon_test`
--
CREATE DATABASE IF NOT EXISTS `dungeon_test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `dungeon_test`;

-- --------------------------------------------------------

--
-- Table structure for table `contents`
--

CREATE TABLE `contents` (
  `id` int(11) NOT NULL,
  `rooms` int(11) NOT NULL,
  `items` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `inventory`
--

CREATE TABLE `inventory` (
  `id` int(11) NOT NULL,
  `items` int(11) NOT NULL,
  `pcs` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `inventory`
--

INSERT INTO `inventory` (`id`, `items`, `pcs`) VALUES
(1, 21, 11),
(2, 22, 11),
(3, 4, 4),
(4, 5, 4),
(5, 11, 8),
(6, 12, 8),
(7, 20, 12),
(8, 21, 12),
(9, 6, 4),
(10, 7, 4);

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `special` varchar(255) NOT NULL,
  `magic` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `loot`
--

CREATE TABLE `loot` (
  `id` int(11) NOT NULL,
  `items` int(11) NOT NULL,
  `npcs` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `loot`
--

INSERT INTO `loot` (`id`, `items`, `npcs`) VALUES
(1, 19, 11),
(2, 20, 11),
(3, 2, 4),
(4, 3, 4),
(5, 9, 8),
(6, 10, 8),
(7, 18, 12),
(8, 19, 12),
(9, 4, 4),
(10, 5, 4);

-- --------------------------------------------------------

--
-- Table structure for table `npcs`
--

CREATE TABLE `npcs` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `hp` int(11) NOT NULL,
  `ac` int(11) NOT NULL,
  `damage` int(11) NOT NULL,
  `lvl` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `pcs`
--

CREATE TABLE `pcs` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `hp` int(11) NOT NULL,
  `ac` int(11) NOT NULL,
  `damage` int(11) NOT NULL,
  `lvl` int(11) NOT NULL,
  `exp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `short_description` varchar(255) NOT NULL,
  `full_description` varchar(255) NOT NULL,
  `light` tinyint(1) NOT NULL,
  `commands` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `contents`
--
ALTER TABLE `contents`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `inventory`
--
ALTER TABLE `inventory`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `loot`
--
ALTER TABLE `loot`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `npcs`
--
ALTER TABLE `npcs`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pcs`
--
ALTER TABLE `pcs`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `contents`
--
ALTER TABLE `contents`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `inventory`
--
ALTER TABLE `inventory`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `loot`
--
ALTER TABLE `loot`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `npcs`
--
ALTER TABLE `npcs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `pcs`
--
ALTER TABLE `pcs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `rooms`
--
ALTER TABLE `rooms`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
