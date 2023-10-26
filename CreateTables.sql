CREATE TABLE `state` (
  `id` integer PRIMARY KEY,
  `name` varchar(50)
);

CREATE TABLE `residential_complex` (
  `id` integer PRIMARY KEY,
  `state_id` integer,
  `name` varchar(255)
);

CREATE TABLE `building` (
  `id` integer PRIMARY KEY,
  `state_id` integer,
  `residential_complex_id` integer,
  `name` varchar(255)
);

CREATE TABLE `apartment` (
  `id` integer PRIMARY KEY,
  `state_id` integer,
  `building_id` integer,
  `name` varchar(100)
);

CREATE TABLE `tenant` (
  `id` integer PRIMARY KEY,
  `name` varchar(100),
  `surname` varchar(100),
  `birthday` datetime,
  `phone` varchar(20),
  `email` varchar(100),
  `passport_series` varchar(4),
  `passport_number` varchar(6)
);

CREATE TABLE `occupancy_type` (
  `id` integer PRIMARY KEY,
  `name` varchar(50)
);

CREATE TABLE `occupancy` (
  `id` integer PRIMARY KEY,
  `tenant_id` integer,
  `apartment_id` integer,
  `date_begin` datetime,
  `date_end` datetime,
  `occupancy_type_id` integer
);

CREATE TABLE `payment_type` (
  `id` integer PRIMARY KEY,
  `name` varchar(50)
);

CREATE TABLE `payment` (
  `id` integer PRIMARY KEY,
  `apartment_id` integer,
  `amount` double,
  `date` datetime,
  `payment_type_id` integer,
  `description` text
);

CREATE TABLE `disbursement` (
  `id` integer PRIMARY KEY,
  `payment_id` integer,
  `tenant_id` integer,
  `amount` double,
  `date` datetime
);

CREATE TABLE `position` (
  `id` integer PRIMARY KEY,
  `name` varchar(100)
);

CREATE TABLE `employee` (
  `id` integer PRIMARY KEY,
  `name` varchar(100),
  `surname` varchar(100),
  `birthday` datetime,
  `phone` varchar(20),
  `email` varchar(100),
  `passport_series` varchar(4),
  `passport_number` varchar(6)
);

CREATE TABLE `employee_position` (
  `id` integer PRIMARY KEY,
  `employee_id` integer,
  `position_id` integer,
  `date` datetime
);

CREATE TABLE `problem_status` (
  `id` integer PRIMARY KEY,
  `name` varchar(20)
);

CREATE TABLE `problem_priority` (
  `id` integer PRIMARY KEY,
  `name` varchar(20)
);

CREATE TABLE `place` (
  `id` integer PRIMARY KEY,
  `apartment_id` integer,
  `building_id` integer,
  `residential_complex_id` integer
);

CREATE TABLE `problem` (
  `id` integer PRIMARY KEY,
  `place_id` integer,
  `date_added` datetime,
  `date_completed` datetime,
  `description` text,
  `problem_priority_id` integer,
  `problem_status_id` integer
);

CREATE TABLE `purpose` (
  `id` integer PRIMARY KEY,
  `problem` integer,
  `employee` integer
);

CREATE TABLE `authentication` (
  `id` integer PRIMARY KEY,
  `login` varchar(50),
  `password` varchar(50)
);

CREATE TABLE `position_in_system` (
  `id` integer PRIMARY KEY,
  `name` varchar(100)
);

CREATE TABLE `employee_position_in_system` (
  `id` integer PRIMARY KEY,
  `employee_id` integer,
  `position_in_system_id` integer
);

ALTER TABLE `residential_complex` ADD FOREIGN KEY (`state_id`) REFERENCES `state` (`id`);

ALTER TABLE `building` ADD FOREIGN KEY (`state_id`) REFERENCES `state` (`id`);

ALTER TABLE `building` ADD FOREIGN KEY (`residential_complex_id`) REFERENCES `residential_complex` (`id`);

ALTER TABLE `apartment` ADD FOREIGN KEY (`state_id`) REFERENCES `state` (`id`);

ALTER TABLE `apartment` ADD FOREIGN KEY (`building_id`) REFERENCES `building` (`id`);

ALTER TABLE `occupancy` ADD FOREIGN KEY (`tenant_id`) REFERENCES `tenant` (`id`);

ALTER TABLE `occupancy` ADD FOREIGN KEY (`apartment_id`) REFERENCES `apartment` (`id`);

ALTER TABLE `occupancy` ADD FOREIGN KEY (`occupancy_type_id`) REFERENCES `occupancy_type` (`id`);

ALTER TABLE `payment` ADD FOREIGN KEY (`apartment_id`) REFERENCES `apartment` (`id`);

ALTER TABLE `payment` ADD FOREIGN KEY (`payment_type_id`) REFERENCES `payment_type` (`id`);

ALTER TABLE `disbursement` ADD FOREIGN KEY (`payment_id`) REFERENCES `payment` (`id`);

ALTER TABLE `disbursement` ADD FOREIGN KEY (`tenant_id`) REFERENCES `tenant` (`id`);

ALTER TABLE `employee_position` ADD FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`);

ALTER TABLE `employee_position` ADD FOREIGN KEY (`position_id`) REFERENCES `position` (`id`);

ALTER TABLE `place` ADD FOREIGN KEY (`apartment_id`) REFERENCES `apartment` (`id`);

ALTER TABLE `place` ADD FOREIGN KEY (`building_id`) REFERENCES `building` (`id`);

ALTER TABLE `place` ADD FOREIGN KEY (`residential_complex_id`) REFERENCES `residential_complex` (`id`);

ALTER TABLE `problem` ADD FOREIGN KEY (`place_id`) REFERENCES `place` (`id`);

ALTER TABLE `problem` ADD FOREIGN KEY (`problem_priority_id`) REFERENCES `problem_priority` (`id`);

ALTER TABLE `problem` ADD FOREIGN KEY (`problem_status_id`) REFERENCES `problem_status` (`id`);

ALTER TABLE `purpose` ADD FOREIGN KEY (`problem`) REFERENCES `problem` (`id`);

ALTER TABLE `purpose` ADD FOREIGN KEY (`employee`) REFERENCES `employee` (`id`);

ALTER TABLE `authentication` ADD FOREIGN KEY (`id`) REFERENCES `employee` (`id`);

ALTER TABLE `employee_position_in_system` ADD FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`);

ALTER TABLE `employee_position_in_system` ADD FOREIGN KEY (`position_in_system_id`) REFERENCES `position_in_system` (`id`);
