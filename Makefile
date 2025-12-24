CXX = g++
CXXFLAGS = -std=c++17 -Wall -Wextra
TARGET = app_launcher
SOURCE = app_launcher.cpp

# Для macOS
ifeq ($(shell uname), Darwin)
	CXXFLAGS += -D__APPLE__
endif

# Для Windows (если используется MinGW)
ifeq ($(OS), Windows_NT)
	CXXFLAGS += -D_WIN32
endif

all: $(TARGET)

$(TARGET): $(SOURCE)
	$(CXX) $(CXXFLAGS) -o $(TARGET) $(SOURCE)

clean:
	rm -f $(TARGET) $(TARGET).exe

run: $(TARGET)
	./$(TARGET)

.PHONY: all clean run

