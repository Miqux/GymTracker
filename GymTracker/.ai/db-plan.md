# Schemat bazy danych dla Gym Tracker

## 1. Lista tabel z kolumnami, typami danych i ograniczeniami

### Users
- **Id**: `int` (PK, autoinkrementacja)
- **Email**: `nvarchar(255)` (NOT NULL, unikalny)
- **PasswordHash**: `nvarchar(max)` (NOT NULL)

### Exercises
- **Id**: `int` (PK, autoinkrementacja)
- **UserId**: `int` (NOT NULL, FK do Users(Id))
- **Name**: `nvarchar(150)` (NOT NULL, długość 3-150 znaków, CHECK CONSTRAINT)
- **MuscleGroup**: `nvarchar(50)` (NOT NULL)
- **DifficultyLevel**: `nvarchar(20)` (NOT NULL) 
- **Description**: `nvarchar(max)` (NULL lub NOT NULL w zależności od potrzeb)
- **IsBlocked**: `bit` (NOT NULL)

**Ograniczenia dodatkowe w tabeli Exercises:**
- Unikalny indeks: `(UserId, Name)`

### Workouts
- **Id**: `int` (PK, autoinkrementacja)
- **UserId**: `int` (NOT NULL, FK do Users(Id))
- **Date**: `date` (NOT NULL)
- **Notes**: `nvarchar(max)` (NULL)

### WorkoutExercises
- **Id**: `int` (PK, autoinkrementacja)
- **WorkoutId**: `int` (NOT NULL, FK do Workouts(Id))
- **ExerciseId**: `int` (NOT NULL, FK do Exercises(Id))
- **Sets**: `int` (NOT NULL)
- **Reps**: `int` (NOT NULL)
- **Weight**: `decimal(6,2)` (NULL - opcjonalny)

## 2. Relacje między tabelami

- **Users → Exercises**: relacja 1 do wielu (jeden użytkownik może mieć wiele ćwiczeń)
- **Users → Workouts**: relacja 1 do wielu (jeden użytkownik może mieć wiele treningów)
- **Workouts → WorkoutExercises**: relacja 1 do wielu (jeden trening zawiera wiele ćwiczeń)
- **Exercises → WorkoutExercises**: relacja 1 do wielu (jedno ćwiczenie może być wykorzystane w wielu treningach)

## 3. Indeksy

- Unikalny indeks na tabeli **Exercises** dla kombinacji `(UserId, Name)`
- Unikalny indeks na kolumnie **Email** w tabeli **Users**

## 4. Zasady MSSQL

- **CHECK Constraint** w tabeli **Exercises** dla kolumny `Name` zapewniający długość nazwy (3-150 znaków)
- W mechanizmie autoryzacji bezpieczeństwo danych kontrolowane jest przez aplikację (JWT, filtrowanie po UserId); na poziomie bazy nie stosujemy zaawansowanych mechanizmów RLS.

## 5. Dodatkowe uwagi

- Propagacja zmian w edycji ćwiczeń odbywa się poprzez przechowywanie referencji (ExerciseId) w tabeli **WorkoutExercises**.
- Schemat jest zaprojektowany zgodnie z zasadami 3NF celem zapewnienia integralności i skalowalności danych.
