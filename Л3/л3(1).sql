-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Пациент`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Пациент` (
  `ФИО` VARCHAR(45) NOT NULL,
  `Дата_рождения` DATE NOT NULL,
  `Полис` INT NOT NULL,
  PRIMARY KEY (`Полис`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Специализация`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Специализация` (
  `idСпециализации` INT NOT NULL,
  `Специальность` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idСпециализации`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Должность`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Должность` (
  `idДолжность` INT NOT NULL,
  `Должность` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idДолжность`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Врач`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Врач` (
  `idВрач` INT NOT NULL,
  `ФИО` VARCHAR(45) NOT NULL,
  `Дата_рождения` DATE NOT NULL,
  `idСпециализации` INT NOT NULL,
  `Должность_idДолжность` INT NOT NULL,
  PRIMARY KEY (`idВрач`),
  INDEX `fk_Врач_Карта врача1_idx` (`idСпециализации` ASC) VISIBLE,
  INDEX `fk_Врач_Должность1_idx` (`Должность_idДолжность` ASC) VISIBLE,
  CONSTRAINT `fk_Врач_Карта врача1`
    FOREIGN KEY (`idСпециализации`)
    REFERENCES `mydb`.`Специализация` (`idСпециализации`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Врач_Должность1`
    FOREIGN KEY (`Должность_idДолжность`)
    REFERENCES `mydb`.`Должность` (`idДолжность`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Диагноз`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Диагноз` (
  `idДиагноз` INT NOT NULL,
  `Описание` VARCHAR(45) NULL,
  PRIMARY KEY (`idДиагноз`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Прием`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Прием` (
  `Дата_приема` DATE NOT NULL,
  `Диагноз_idДиагноз` INT NOT NULL,
  `Пациент_Полис` INT NOT NULL,
  `Врач_idВрач` INT NOT NULL,
  `idПриема` VARCHAR(45) NOT NULL,
  INDEX `fk_Прием_Диагноз1_idx` (`Диагноз_idДиагноз` ASC) VISIBLE,
  INDEX `fk_Прием_Пациент1_idx` (`Пациент_Полис` ASC) VISIBLE,
  INDEX `fk_Прием_Врач1_idx` (`Врач_idВрач` ASC) VISIBLE,
  PRIMARY KEY (`idПриема`),
  CONSTRAINT `fk_Прием_Диагноз1`
    FOREIGN KEY (`Диагноз_idДиагноз`)
    REFERENCES `mydb`.`Диагноз` (`idДиагноз`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Прием_Пациент1`
    FOREIGN KEY (`Пациент_Полис`)
    REFERENCES `mydb`.`Пациент` (`Полис`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Прием_Врач1`
    FOREIGN KEY (`Врач_idВрач`)
    REFERENCES `mydb`.`Врач` (`idВрач`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
