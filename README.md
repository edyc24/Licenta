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

**UML Diagram**

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
