### Use-cases
The following use cases are the foundation for the application:
- As a system user, I want to be able to schedule a questionnaire so that I can ask my target group about a
topic/case.
- As the owner of a scheduled questionnaire, I want to be able to reschedule the questionnaire as long as it has
not started.
- As the owner of an active questionnaire, I want to be able to close the questionnaire at will.

### Static structure
A questionnaire should at least consist of the following:
- Name
- Startdate/time
- Enddate/time
- Status

### Business rules
The following business-rules are applicable:
- A questionnaire can only be scheduled for a future date/time.
- No "retroactive" changes are allowed.
A questionnaire is planned for the future and will be completed in the future.
- The end date and time are at least one hour after the beginning date and time.
- A questionnaire will always exist in one of the following states:
- Concept: The questionnaire is intentionally inactive, and cannot be administered.
- Scheduled: The start date and time of the questionnaire are in the future. No questions can be answered yet.
Active: the questionnaire's start date and time are in the past, while its end date and time are in the future. Only in this state can the questions be answered.

- Completed: The questionnaire's end date and time have passed. No more questions can be answered.

The mentioned statuses are sequential, never skip a step, and cannot be reversed.

### Interface
Build at least the following screens:
- Create-screen for a questionnaire
- Update-screen for a questionnaire
- Overview-screen of questionnaires
  - Display at least the name, startdate/time and enddate/time
  - Sort the overview-screen by startdate/time, enddate/time (by default)
  - Create an intuitive search-function for questionnaires
- A stub for the "answering-screen"; it is in this stage sufficient to display the questionnaire status.