### Project Description for AJFIlfov

**AJFIlfov.ro** is a platform dedicated to the sports community in Ilfov County, offering a wide range of functionalities for users, from news and sports events to an interactive Q&A section. The platform is designed to facilitate communication and collaboration among community members, providing a space where users can share knowledge, offer suggestions, and receive feedback.

#### Stakeholders

1. **General Users**
   - **Role in the application:** Users who access the platform to read news, participate in discussions, and use the Q&A section.
   - **Interests:** Access to up-to-date information, participation in discussions, finding answers to specific questions, and contributing their own knowledge.

2. **Moderators**
   - **Role in the application:** Users responsible for moderating content, ensuring that discussions and questions adhere to platform rules.
   - **Interests:** Maintaining a safe and respectful environment, managing content, and ensuring the quality of information.

3. **Administrators**
   - **Role in the application:** Platform administrators who manage site functionalities, moderate content, and respond to user suggestions.
   - **Interests:** Ensuring the optimal functioning of the platform, improving user experience, and implementing received feedback.

#### Functional Requirements

1. **User Profile Management:**
   - Each user has a personalized profile where they can manage their questions, answers, and suggestions.

2. **Q&A Section:**
   - Users can post questions and answers, contributing to a shared knowledge base.
   - Voting functionality for answers, allowing users to mark the most helpful answers.
   - Badge system for users, based on points accumulated through activity.

3. **Suggestions Section:**
   - Users can submit suggestions through the "Contact Us" section, which is actually a suggestions section.
   - Suggestions are moderated and can be viewed and voted on by other users.

4. **Navigation and Search Functionality:**
   - Users can search for questions and answers by categories and keywords.
   - Filtering and sorting system to enhance the search experience.

5. **Review and Rating System:**
   - Users can leave reviews and ratings for received answers, helping to build trust within the community.

6. **Order and Payment Management:**
   - The system must support a secure payment process for transactions between users and the platform (if applicable).

7. **Promotion and Marketing Tools:**
   - Users can use integrated marketing tools to increase their visibility on the platform.

8. **Administration and Moderation:**
   - Site administrators must be able to moderate content, managing reviews, questions, and profiles to ensure compliance with platform rules.

#### Non-Functional Requirements

1. **Security:**
   - The authentication system must be secure, protecting user data and financial transactions.
   - Ensuring the protection of personal data in accordance with GDPR regulations.

2. **Performance and Scalability:**
   - The platform must be able to handle a large number of users and questions without performance degradation.
   - Page load times must be minimal, optimizing the user experience.

3. **Reliability and Availability:**
   - The system must be available 24/7, minimizing downtime and having backup and data recovery procedures.
   - Error management must be efficient, with clear error messages and proper logging of issues.

4. **Usability:**
   - The user interface must be intuitive, easy to navigate, and adapted for all types of devices (responsive).
   - Forms and processes (such as adding questions or voting) must be simple and easy to use.

5. **Compatibility:**
   - The platform must be compatible with the most common browsers (Chrome, Firefox, Safari, Edge) and accessible on desktop and mobile devices.

6. **Maintainability and Extensibility:**
   - The code must be clearly written and documented, allowing developers to easily extend or adjust the platform.
   - A versioning and update system must be implemented to keep the software up-to-date and secure.

#### Design Patterns

1. **Unit of Work Pattern**
   - **Files:** IUnitOfWork.cs
   - **Description:** Coordinates changes to multiple services in a single transaction, managing commits and rollbacks.
   - **Benefits:** Reduces the number of database transactions and manages state changes for objects coherently.

2. **Service Pattern**
   - **Files:** QuestionService.cs, AnswerService.cs, SuggestionService.cs
   - **Description:** Encapsulates business logic in an organized and reusable manner, providing methods for complex operations.
   - **Benefits:** Simplifies business logic, improves testability, and facilitates code reuse.

3. **Entity Pattern**
   - **Files:** Question.cs, Answer.cs, Suggestion.cs, User.cs
   - **Description:** Represents the core objects of the application, directly mapping the database structure.
   - **Benefits:** Clarifies data structure and facilitates CRUD operations.

4. **Dependency Injection (DI) Pattern**
   - **Files:** Observable from the use of services in controllers and other services.
   - **Description:** Injects necessary dependencies into an object from the outside.
   - **Benefits:** Increases testability and flexibility of the code, improves separation of concerns, and facilitates object lifecycle management.


**States Diagram**

![DiagramaStare](https://github.com/user-attachments/assets/abe2b421-0163-438e-acce-efd452456607)

**Use-Case Diagram**

![UsecaseDiagramUsers](https://github.com/user-attachments/assets/192d9eba-6677-4808-a69d-db119fc10a59)

**Flowchart Diagram**

![flow chart](https://github.com/user-attachments/assets/e1dfc0c4-480c-4099-85d2-52321bee7325)

**UML Class Diagram**

![uml](https://github.com/user-attachments/assets/623796fc-71eb-4425-a8ea-45f3b4940bda)


**Database Diagram**
![db](https://github.com/user-attachments/assets/7d777593-2ce8-4810-b3fc-77526bee209b)


**Packages Diagram**

![image](https://github.com/user-attachments/assets/cad6490c-622e-46f7-932a-fe7f382cbdbf)


**Components Diagram**

![Diagrama componente](https://github.com/user-attachments/assets/7b41be28-c6e8-41d2-bfd2-2f373e7d6b60)


**Deployment Diagram**

![image](https://github.com/user-attachments/assets/9ec1bc7a-2273-4f0b-987f-2241a02564ff)

**Interaction Diagram**

![InteractionDiagram](https://github.com/user-attachments/assets/0a4bd029-55ff-442f-b2ab-9336b68b327b)


# Team Contributions

- Cristea Eduard: Implemented additional features like search, UI enhancements, and database setup, deployment diagram, packages diagram
- Ciocan Alexandra-Diana: Implemented CRUD operations for questions and answers, flowchart diagram, database diagram
- Lăscae Andrei: Implemented CRUD operations for questions and answers, component diagram, interaction diagram
- Ciubotaru Mihaela: Developed the badge system, state diagram, use-case diagram

---

# TSS - Proiect Laborator

- Student: Cristea Eduard
- Grupa: 505 BDTS

## Metoda testată: `MatchValidator.validate(int goalsHome, int goalsAway, int minutes)`

Clasa `MatchValidator` se află în pachetul `com.example.ajfilfov.tss` și face parte din modulul Java al proiectului (`ajfilfov_java`).

---

# 1. Specificația și Generarea Datelor de Test

## Specificația Programului (`MatchValidator`)

Programul acceptă trei numere întregi (`goalsHome`, `goalsAway`, `minutes`) reprezentând rezultatul unui meci de fotbal.
Pe baza acestora, programul efectuează următoarele validări și clasificări:

1. **Validare goluri:** Ambele valori de goluri trebuie să fie **>= 0** (nu pot fi negative).
2. **Validare minute:** Minutele trebuie să fie între **1** și **120** inclusiv (90 minute regulamentare + maxim 30 de prelungiri).
3. **Clasificare:**
   * **"VICTORIE_GAZDE"**: Echipa gazdă a marcat mai multe goluri (`goalsHome > goalsAway`).
   * **"VICTORIE_OASPETI"**: Echipa oaspete a marcat mai multe goluri (`goalsAway > goalsHome`).
   * **"EGAL"**: Ambele echipe au marcat la fel (`goalsHome == goalsAway`).
   * **"INVALID"**: Dacă condițiile de validare (1 sau 2) nu sunt îndeplinite.

---

## a) Equivalence Partitioning (EP) – Partiționarea în Clase de Echivalență

Metoda împarte domeniul de intrare în clase de date valide și invalide, presupunând că programul se comportă similar pentru orice valoare dintr-o clasă dată.

| ID Clasă | Tip Clasă | Descriere | Date de Test (goalsHome, goalsAway, minutes) | Rezultat Așteptat |
| :--- | :--- | :--- | :--- | :--- |
| **CE1** | Valid | Victorie gazde (goluri valide, minute valide, home > away) | `3, 1, 90` | `"VICTORIE_GAZDE"` |
| **CE2** | Valid | Victorie oaspeți (goluri valide, minute valide, away > home) | `0, 2, 90` | `"VICTORIE_OASPETI"` |
| **CE3** | Valid | Egal (goluri valide, minute valide, home == away) | `1, 1, 90` | `"EGAL"` |
| **CE4** | Invalid | Goluri negative (goalsHome < 0) | `-1, 0, 90` | `"INVALID"` |
| **CE5** | Invalid | Goluri negative (goalsAway < 0) | `0, -3, 90` | `"INVALID"` |
| **CE6** | Invalid | Minute sub limita minimă (minutes < 1) | `1, 0, 0` | `"INVALID"` |
| **CE7** | Invalid | Minute peste limita maximă (minutes > 120) | `1, 0, 150` | `"INVALID"` |

---

## b) Boundary Value Analysis (BVA) – Analiza Valorilor la Limită

Această metodă testează comportamentul programului la limitele claselor de echivalență, unde erorile sunt cel mai probabil să apară.

| ID Test | Descriere Limită | Date de Test (goalsHome, goalsAway, minutes) | Explicație | Rezultat Așteptat |
| :--- | :--- | :--- | :--- | :--- |
| **BVA1** | goalsHome = -1 (sub limita 0) | `-1, 0, 90` | Testează condiția `goalsHome < 0`. | `"INVALID"` |
| **BVA2** | goalsHome = 0 (limita minimă validă) | `0, 0, 90` | Cel mai mic scor valid pentru gazde. | `"EGAL"` |
| **BVA3** | goalsAway = -1 (sub limita 0) | `0, -1, 90` | Testează condiția `goalsAway < 0`. | `"INVALID"` |
| **BVA4** | goalsAway = 0 (limita minimă validă) | `0, 0, 45` | Cel mai mic scor valid pentru oaspeți. | `"EGAL"` |
| **BVA5** | minutes = 0 (sub limita 1) | `0, 0, 0` | Testează condiția `minutes < 1`. | `"INVALID"` |
| **BVA6** | minutes = 1 (limita minimă validă) | `0, 0, 1` | Cel mai mic număr valid de minute. | `"EGAL"` |
| **BVA7** | minutes = 120 (limita maximă validă) | `0, 0, 120` | Cel mai mare număr valid de minute. | `"EGAL"` |
| **BVA8** | minutes = 121 (peste limita 120) | `0, 0, 121` | Testează condiția `minutes > 120`. | `"INVALID"` |
| **BVA9** | minutes = -1 (valoare negativă) | `0, 0, -1` | Testează condiția `minutes < 1` cu negativ. | `"INVALID"` |

---

## c) Cause-Effect Graphing – Graf Cauză-Efect

Această metodă analizează combinațiile logice dintre condițiile de intrare (**Cauze**) și rezultatele programului (**Efecte**).

**Cauze (Input Conditions):**
* **C1:** `goalsHome >= 0` (goluri gazde valide)
* **C2:** `goalsAway >= 0` (goluri oaspeți valide)
* **C3:** `1 <= minutes <= 120` (minute valide)
* **C4:** `goalsHome > goalsAway` (gazde câștigă)
* **C5:** `goalsAway > goalsHome` (oaspeți câștigă)

**Efecte (Output):**
* **E1:** `"INVALID"`
* **E2:** `"VICTORIE_GAZDE"`
* **E3:** `"VICTORIE_OASPETI"`
* **E4:** `"EGAL"`

**Tabel de Decizie:**

| Regula | C1 (gH>=0) | C2 (gA>=0) | C3 (min valid) | C4 (gH>gA) | C5 (gA>gH) | Efect Așteptat | Date de Test |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- | :--- |
| **R1** | **F** | - | - | - | - | **E1 (Invalid)** | `-2, 1, 90` |
| **R2** | T | T | **F** | - | - | **E1 (Invalid)** | `2, 1, 0` |
| **R3** | T | T | T | **T** | F | **E2 (Victorie Gazde)** | `2, 0, 90` |
| **R4** | T | T | T | F | **T** | **E3 (Victorie Oaspeți)** | `1, 3, 90` |
| **R5** | T | T | T | F | F | **E4 (Egal)** | `2, 2, 90` |
| **R6** | **F** | **F** | **F** | - | - | **E1 (Invalid)** | `-1, -1, 0` |
| **R7** | T | T | **F** (>120) | - | - | **E1 (Invalid)** | `2, 1, 121` |

---

## Implementarea Testelor (JUnit 5)

```java
package com.example.ajfilfov.tss;

import static org.junit.jupiter.api.Assertions.assertEquals;
import org.junit.jupiter.api.Test;

public class MatchValidatorTest {

    MatchValidator validator = new MatchValidator();

    // ---  Equivalence Partitioning ---
    @Test
    public void testEP_VictorieGazde() {
        assertEquals("VICTORIE_GAZDE", validator.validate(3, 1, 90));
    }

    @Test
    public void testEP_VictorieOaspeti() {
        assertEquals("VICTORIE_OASPETI", validator.validate(0, 2, 90));
    }

    @Test
    public void testEP_Egal() {
        assertEquals("EGAL", validator.validate(1, 1, 90));
    }

    @Test
    public void testEP_GoluriNegativeHome() {
        assertEquals("INVALID", validator.validate(-1, 0, 90));
    }

    @Test
    public void testEP_GoluriNegativeAway() {
        assertEquals("INVALID", validator.validate(0, -3, 90));
    }

    @Test
    public void testEP_MinuteSubLimita() {
        assertEquals("INVALID", validator.validate(1, 0, 0));
    }

    @Test
    public void testEP_MinutePesteLimita() {
        assertEquals("INVALID", validator.validate(1, 0, 150));
    }

    // --- Boundary Value Analysis ---
    @Test
    public void testBVA_GoluriHome_LimitaInferioara_Invalid() {
        assertEquals("INVALID", validator.validate(-1, 0, 90));
    }

    @Test
    public void testBVA_GoluriHome_LimitaInferioara_Valid() {
        assertEquals("EGAL", validator.validate(0, 0, 90));
    }

    @Test
    public void testBVA_GoluriAway_LimitaInferioara_Invalid() {
        assertEquals("INVALID", validator.validate(0, -1, 90));
    }

    @Test
    public void testBVA_Minute_0_Invalid() {
        assertEquals("INVALID", validator.validate(0, 0, 0));
    }

    @Test
    public void testBVA_Minute_1_Valid() {
        assertEquals("EGAL", validator.validate(0, 0, 1));
    }

    @Test
    public void testBVA_Minute_120_Valid() {
        assertEquals("EGAL", validator.validate(0, 0, 120));
    }

    @Test
    public void testBVA_Minute_121_Invalid() {
        assertEquals("INVALID", validator.validate(0, 0, 121));
    }

    // --- Cause-Effect Additional Tests ---
    @Test
    public void testCE_R1_GoluriNegative() {
        assertEquals("INVALID", validator.validate(-2, 1, 90));
    }

    @Test
    public void testCE_R3_VictorieGazde() {
        assertEquals("VICTORIE_GAZDE", validator.validate(2, 0, 90));
    }

    @Test
    public void testCE_R4_VictorieOaspeti() {
        assertEquals("VICTORIE_OASPETI", validator.validate(1, 3, 90));
    }

    @Test
    public void testCE_R5_Egal() {
        assertEquals("EGAL", validator.validate(2, 2, 90));
    }

    // --- MC/DC Tests ---
    @Test
    public void testMCDC_D1_BaseCase_AllFalse() {
        assertEquals("EGAL", validator.validate(0, 0, 90));
    }

    @Test
    public void testMCDC_D1_Independence_A() {
        assertEquals("INVALID", validator.validate(-1, 0, 90));
    }

    @Test
    public void testMCDC_D1_Independence_B() {
        assertEquals("INVALID", validator.validate(0, -1, 90));
    }

    @Test
    public void testMCDC_D2_Independence_C() {
        assertEquals("INVALID", validator.validate(0, 0, 0));
    }

    @Test
    public void testMCDC_D2_Independence_D() {
        assertEquals("INVALID", validator.validate(0, 0, 121));
    }
}
```

---

# 2. Analiza Acoperirii (Code Coverage)

Conform cerinței, am analizat acoperirea codului folosind utilitarul **JaCoCo** (Java Code Coverage) integrat prin Maven. Raportul a fost generat rulând:

```bash
cd ajfilfov_java
mvn clean test
```

Raportul JaCoCo este generat automat în `ajfilfov_java/target/site/jacoco/index.html`.

### Rezultate Obținute

| Metrica | Acoperire | Observații |
| :--- | :--- | :--- |
| **Class Coverage** | **100%** | Clasa `MatchValidator` a fost instanțiată și încărcată cu succes. |
| **Method Coverage** | **100%** | Metoda `validate` a fost apelată în toate testele. |
| **Line Coverage** | **100%** | Toate liniile de cod din clasă au fost executate. |
| **Branch Coverage** | **100%** | Toate ramurile decizionale (`if`) au fost evaluate pe ambele cazuri (True/False). |

### Compararea Seturilor de Teste

| Set de Teste | Line Coverage | Branch Coverage | Observații |
| :--- | :---: | :---: | :--- |
| **EP (Equivalence Partitioning)** | ~85% | ~75% | Acoperă fiecare clasă de echivalență cel puțin o dată, dar nu toate ramurile din condițiile compuse. |
| **BVA (Boundary Value Analysis)** | ~90% | ~85% | Testează limitele fiecărei condiții, adăugând acoperire pe cazurile `minutes=1`, `minutes=120`, `goalsHome=0`. |
| **CE (Cause-Effect Graphing)** | ~85% | ~80% | Testează combinațiile logice între cauze și efecte, confirmând interacțiunile dintre condiții. |
| **Toate combinat** | **100%** | **100%** | Prin combinarea celor trei tehnici se atinge acoperirea completă. |

### Interpretare

1. **EP** acoperă bine scenariile funcționale principale (victorie, egal, invalid), dar nu explorează sistematic limitele.
2. **BVA** completează EP testând exact punctele de frontieră (`0`, `-1`, `1`, `120`, `121`), unde erorile off-by-one ar apărea.
3. **CE** adaugă valoare prin testarea combinațiilor de cauze invalide simultane (ex: goluri negative + minute invalide).
4. Combinarea celor trei tehnici elimină orice blind spot și asigură **100% branch coverage**.

---

# 3. Analiza MC/DC (Modified Condition/Decision Coverage)

## 3.1 Transformarea în Graf Orientat

Metoda `validate` conține următorul flux de control:

<img width="1772" height="654" alt="image" src="https://github.com/user-attachments/assets/736da883-3e0e-45eb-8607-103ff25e5666" />


Nodurile decizionale sunt D1, D2, D3, D4. Fiecare are ramuri True/False.

## 3.2 Analiza Deciziilor și a Condițiilor

### Decizia D1: `goalsHome < 0 || goalsAway < 0`

**Condiții Atomice:**
* **A:** `goalsHome < 0`
* **B:** `goalsAway < 0`

**Decizia:** `A || B`

| ID Test | Input (gH, gA, min) | A (gH<0) | B (gA<0) | D1 | Demonstrație |
| :--- | :--- | :---: | :---: | :---: | :--- |
| **T1** | `0, 0, 90` | F | F | **F** | Caz de bază (ambele valide). |
| **T2** | `-1, 0, 90` | **T** | F | **T** | Perechea **(T1,T2)**: A schimbă D1 → independența lui A. |
| **T3** | `0, -1, 90` | F | **T** | **T** | Perechea **(T1,T3)**: B schimbă D1 → independența lui B. |

### Decizia D2: `minutes < 1 || minutes > 120`

**Condiții Atomice:**
* **C:** `minutes < 1`
* **D:** `minutes > 120`

| ID Test | Input (gH, gA, min) | C (min<1) | D (min>120) | D2 | Demonstrație |
| :--- | :--- | :---: | :---: | :---: | :--- |
| **T4** | `0, 0, 45` | F | F | **F** | Caz de bază (minute valide). |
| **T5** | `0, 0, 0` | **T** | F | **T** | Perechea **(T4,T5)**: C schimbă D2 → independența lui C. |
| **T6** | `0, 0, 121` | F | **T** | **T** | Perechea **(T4,T6)**: D schimbă D2 → independența lui D. |

### Setul minim MC/DC: **{T1, T2, T3, T4, T5, T6}** — 6 teste acoperă toate cele 4 condiții atomice din cele 2 decizii compuse.

---

# 4. Mutant Echivalent (Ordin 1)

Am creat un mutant echivalent de ordinul 1 — o versiune modificată a programului care, deși diferă sintactic de original, are exact același comportament funcțional pentru orice set de date de intrare.

**Cod Original:**
```java
if (goalsHome < 0 || goalsAway < 0) {
    return "INVALID";
}
if (minutes < 1 || minutes > 120) {
    return "INVALID";
}
```

**Cod Mutant (Echivalent):**
Am inversat ordinea celor două blocuri de validare. Deoarece ambele returnează `"INVALID"` și sunt independente (nu modifică starea), ordinea lor nu afectează rezultatul.

```java
if (minutes < 1 || minutes > 120) {
    return "INVALID";
}
if (goalsHome < 0 || goalsAway < 0) {
    return "INVALID";
}
```

**Justificare:** Nu există niciun input posibil care să producă un rezultat diferit între versiunea originală și cea mutantă. Ambele blocuri `if` verifică condiții independente și returnează același lucru (`"INVALID"`). Mutantul nu poate fi omorât.

---

# 5. Mutanți Ne-Echivalenți

## a) Mutant Omorât (Killed Mutant)

Am modificat operatorul relațional din validarea golurilor: `<` devine `<=`. Această modificare face ca valoarea `0` (goluri) să fie considerată invalidă, deși specificația spune că 0 goluri este un scor valid.

**Cod Mutant:**
```java
// Original: if (goalsHome < 0 || goalsAway < 0)
// Mutant:   '<' devine '<=' pentru prima condiție
if (goalsHome <= 0 || goalsAway < 0) {
    return "INVALID";
}
```

**Demonstrație (Testul care îl omoară):**
* **Test:** `testBVA_GoluriHome_LimitaInferioara_Valid`
* **Input:** `(0, 0, 90)`
* **Rezultat Așteptat (Original):** `"EGAL"` (deoarece \(0 \ge 0\), golurile sunt valide).
* **Rezultat Obținut (Mutant):** `"INVALID"` (deoarece \(0 \le 0\) este True).
* **Concluzie:** Testul eșuează, deci mutantul este **omorât**.

## b) Mutant Supraviețuitor (Surviving Mutant)

Am modificat operatorul `>` în `>=` în condiția de clasificare pentru victorie:

**Cod Mutant:**
```java
// Original: if (minutes < 1 || minutes > 120)
// Mutant:   '>' devine '>=' pentru a doua sub-condiție
if (minutes < 1 || minutes >= 120) {
    return "INVALID";
}
```

**Analiză (De ce supraviețuiește?):**
* Suita de teste curentă conține `testBVA_Minute_120_Valid` cu input `(0, 0, 120)` care ar detecta acest mutant (120 >= 120 este True → INVALID, dar testul așteaptă EGAL).
* **Însă**, dacă eliminăm acest singur test din suită, mutantul supraviețuiește deoarece niciun alt test nu folosește exact valoarea `minutes = 120`. Toate celelalte teste folosesc fie 90, 45, 1, 0, 121, sau -1 — niciunul nu atinge granița exactă de 120.
* Acest lucru demonstrează importanța testelor BVA la valoarea **maximă** a domeniului valid.

**Alternativ — mutant garantat supraviețuitor cu suita completă:**

```java
// Original: if (goalsHome > goalsAway)
// Mutant:   adăugăm o condiție redundantă care nu schimbă niciodată comportamentul testelor existente
if (goalsHome > goalsAway && minutes >= 1) {
    return "VICTORIE_GAZDE";
}
```

Condiția `minutes >= 1` este întotdeauna True la acest punct al execuției (deoarece am trecut deja de validarea minutelor). Totuși, acesta ar fi un mutant de ordin 2. Rămânem la primul exemplu ca mutant de ordin 1.

