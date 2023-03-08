### High-level description
A functional diagram depicting the use case and how application resolves the issue

### Data storage details
Database schema diagram illustrates entities and their relationships.

- Questionnaires table is usually the top-most entity in code relations, but in database it stands aside as long as it has no value by its own
- QuestionnaireSubmissions table is the central entity which connects the actual questionnaire and users as well as their answers to specific survey questions
- Users table has its main connection to questionnaire submissions, but it also has reference to questionnaire directly for cases when user is a questionnaire creator
- QuestionnaireAnswers table contains bits of information (question-answer connection) which together form a complete survey submission
- QuestionnaireItems table contains bits of information (question-answer connection) which together form a complete questionnaire