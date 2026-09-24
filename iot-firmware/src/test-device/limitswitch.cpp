const int doorPins[3] = {27, 32, 23};
const char* doorNames[3] = {"S", "M", "L"};

int stableStates[3];
int lastReadings[3];
unsigned long changedAt[3];

const unsigned long DEBOUNCE_MS = 40;

void printDoor(int index, int state) {
  Serial.print("Door ");
  Serial.print(doorNames[index]);

  if (state == LOW) {
    Serial.println(": CLOSED");
  } else {
    Serial.println(": OPEN");
  }
}

void setup() {
  Serial.begin(115200);

  for (int i = 0; i < 3; i++) {
    pinMode(doorPins[i], INPUT_PULLUP);

    stableStates[i] = digitalRead(doorPins[i]);
    lastReadings[i] = stableStates[i];
    changedAt[i] = 0;

    printDoor(i, stableStates[i]);
  }

  Serial.println("Three door sensors ready");
}

void loop() {
  for (int i = 0; i < 3; i++) {
    int reading = digitalRead(doorPins[i]);

    if (reading != lastReadings[i]) {
      lastReadings[i] = reading;
      changedAt[i] = millis();
    }

    if (
      reading != stableStates[i] &&
      millis() - changedAt[i] >= DEBOUNCE_MS
    ) {
      stableStates[i] = reading;
      printDoor(i, stableStates[i]);
    }
  }
}