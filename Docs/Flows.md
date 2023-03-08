### High-level description
At a current state system supports several flows:
- Admin flow (for questionnaires which he created)
1) Admin can create questionnaire (it automatically receives "Concept" status), required data is "Title" and either valid Start/End time or empty Start/End time (as long as "Concept" questionnaire is not in active pool and has to be modified later)
2) Admin can set a schedule for questionnaire (it automatically receives "Scheduled" status), Start/End time is required on this step (as long as "Scheduled" questionnaire is meant to be fully valid and will automatically start at selected time)
3) Admin can reschedule questionnaire if it has not started yet (status stays "Scheduled"), Start/End time is required on this step
4) Admin can close currently active questionnaire if it was active for at least one hour (it automatically receives "Completed" status)
- Admin/User flow (for questionnaires in general)
1) User can see an overview of existing questionnaires with brief information
2) User can see a button to start filling in questionnaire if its status is "Active"

### Data storage details
Database schema diagram illustrates entities and their relationships.

- Questionnaires table is usually the top-most entity in code relations, but in database it stands aside as long as it has no value by its own
- QuestionnaireSubmissions table is the central entity which connects the actual questionnaire and users as well as their answers to specific survey questions
- Users table has its main connection to questionnaire submissions, but it also has reference to questionnaire directly for cases when user is a questionnaire creator
- QuestionnaireAnswers table contains bits of information (question-answer connection) which together form a complete survey submission
- QuestionnaireItems table contains bits of information (question-answer connection) which together form a complete questionnaire

### Improvements/functionality to be done in future
- implement register/login functionality and user roles validation on UI to hide/restrict create/update actions on UI for regular users
- implement questionnaire items CRUD for Admin on UI
- implement solid user-side informational exchange (validation tooltips, loaders, consistent colors and input types, success/error response handling with toastr/modal notifications)
- implement a form to fill in active questionnaire
- implement email and/or phone for User to allow annonymous users be saved in system so they could login to the system later
- implement history functionality for each User/Admin where created/submitted questionnaires would be displayed
- implement topics functionality to be able to filter questionnaires by interests or restrict access to specfic topics in a "whitelisting" style
- implement dynamically build UI element which would be rendered per each questionnaire item according to it's type