# JSON Test Platform
<img width="459" height="285" alt="image" src="https://github.com/user-attachments/assets/3730b4a6-6286-4bc0-aebf-865b78060149" />


## How to use/Controls:

1. Launch the App,
2. Click on "Test"
3. Click on "Load Test"
<img width="460" height="284" alt="image" src="https://github.com/user-attachments/assets/bc2db2d8-ad74-4ed0-884e-f62d80dcefb4" />

4. Use "Next" and "Previous" to switch questions

### How to mark answers as the right one:

Click on whatever radio button you think is the correct choice

### How to finish the test:
1. Press "Finish"
<img width="459" height="285" alt="image" src="https://github.com/user-attachments/assets/0ab92fdf-9f05-4993-807c-e172fb5854f8" />

2. You get a Popup giving you your grade:
<img width="136" height="132" alt="image" src="https://github.com/user-attachments/assets/16fbd988-f6c9-4a30-b951-7429b402fd71" />

3. Press "Ok" to close the popup and get your results in the main viewer:
<img width="457" height="281" alt="image" src="https://github.com/user-attachments/assets/1d6b12de-5f8a-4260-b2da-92ec5462cb52" />


## How to create a test:
1. Use the Template JSON:
```json
{
  "testTitle": "Sample Test",
"questionText": "",
  "questions": [
    {
      "id": 1,
      "answers": {
        "A": "",
        "B": "",
        "C": "",
        "D": ""
      },
      "correct": ""
    }
  ]
}

```
Id: Sequence of questions, a block with ID = 1 will be the starting question, a block with ID = 2 will be the second and so on, add as may as you want.
"answers" each contain the text that will be shown on the questionaire
correct = the correct choice, questionText is what the question shown to the user will be

Example:
<img width="270" height="93" alt="image" src="https://github.com/user-attachments/assets/ecaee425-4979-4411-802a-75be71845d4a" />


In this example:
A = Earth
B = Mars
C = Jupiter
D = Venus

Question text = Which planet is known as the Red Planet?
















