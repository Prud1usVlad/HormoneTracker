// BraceletApp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <string>
using namespace std;

void readButtonsSignal();
bool loopFunction();
float readAnalysisResult();
void displayMessage(string message);
void requestPin();
void callEmergency();
float readAnalysisResult();
void sentData(string data);
void analyseData();

// simulation variables //
int calls = -1;


// global device variables // 
float minimalResult = 13.2;
float maximalResult = 16.6;
float lastAnalysisResult = 0;

float soundIntensivity = 0;
string ioStream = "";

bool isButton1Pressed = false;
bool isButton2Pressed = false;
bool isButton3Pressed = false;
bool isButton1LongPressed = false;


int main()
{
    bool active = true;
    while (active)
    {
        active = loopFunction();
    }
}

// Головна управляюча функція, виконується в циклі 
bool loopFunction() {
    readButtonsSignal();

    if (isButton1LongPressed) {
        callEmergency();
    }
    else if (isButton1Pressed) {
        displayMessage("Turning off");
        return false;
    }
    else if (isButton2Pressed) {
        requestPin();
        sentData("hemoglobin:" + to_string(lastAnalysisResult));
    }
    else if (isButton3Pressed) {
        displayMessage("Analysis result: " + to_string(lastAnalysisResult));
    }
}

// Емуляція натискання на кнопки
// 1 - перша кнопка натиснута
// 11111 - перша кнопка затиснута
// 2 - друга кнопка натиснута 
// 3 - третя кнопка натиснута
void readButtonsSignal() {
    displayMessage("Buttons input simulation: ");
    char input[10] = "    ";
    cin >> input;

    if (input[4] == '1' && input[0] == '1') {
        isButton1Pressed = false;
        isButton2Pressed = false;
        isButton3Pressed = false;
        isButton1LongPressed = true;
    }
    else if (input[0] == '1') {
        isButton1Pressed = true;
        isButton2Pressed = false;
        isButton3Pressed = false;
        isButton1LongPressed = false;
    }
    else if (input[0] == '2') {
        isButton1Pressed = false;
        isButton2Pressed = true;
        isButton3Pressed = false;
        isButton1LongPressed = false;
    }
    else if (input[0] == '3')
    {
        isButton1Pressed = false;
        isButton2Pressed = false;
        isButton3Pressed = true;
        isButton1LongPressed = false;
    }
    else {
        isButton1Pressed = false;
        isButton2Pressed = false;
        isButton3Pressed = false;
        isButton1LongPressed = false;
    }
}

// Емуляція виводу повідомлення на екран пристрою
void displayMessage(string message) {
    cout << message << endl;
}

// Функція запиту голки, для проведення подальшого аналізу крові
// 1 для підтвердження вставки голки
void requestPin() {
    displayMessage("Please insert pin.");
    char input = ' ';
    cin >> input;

    if (input == '1') {
        lastAnalysisResult = readAnalysisResult();
        displayMessage("Analysis result: " + to_string(lastAnalysisResult));
        analyseData();
    }
}
// Функція аналізу отриманих значень
void analyseData() {
    float minProportion = (minimalResult - lastAnalysisResult) * 100 / minimalResult;
    float maxProportion = (lastAnalysisResult - maximalResult) * 100 / maximalResult;

    if (minProportion <= 0 && maxProportion <= 0) {
        displayMessage("Result is OK!");
    }
    else if (minProportion <= 10 && minProportion > 0 ||
        maxProportion <= 10 && maxProportion > 0) {
        displayMessage("Result has low deviation (10%>)");
    }
    else if (minProportion <= 25 && minProportion > 0 ||
        maxProportion <= 25 && maxProportion > 0) {
        displayMessage("Result has high deviation (25%>)");
    }
    else if (minProportion <= 50 && minProportion > 0 ||
        maxProportion <= 50 && maxProportion > 0) {
        displayMessage("Result has very high deviation (50%>)");
    }
    else {
        displayMessage("Result has DANGEROUS deviation (50%<)!");
    }
}

// Функція надсилання запиту на виклик швидкої 
void callEmergency() {
    sentData("emergency:1");
    soundIntensivity = 1;
}

// Функція емуляції зчитування даних з сенсору аналізатору цукру в крові
float readAnalysisResult() {
    float possibleData[] = { 14, 13, 14, 15, 10, 18, 34 };
    ++calls;
    return possibleData[calls % 7];
}

// Функція емуляції відправки даних через блютуз на 
// мобільний пристрій, що підключений до браслету 
void sentData(string data) {
    ioStream = data;
    cout << "Sent: " << ioStream << endl;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
