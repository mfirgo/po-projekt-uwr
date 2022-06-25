
#change this to the name of the Main class file, without file extension
MAIN_FILE = Main

# compiler
CSHARP_COMPILER = mcs
# compiler flags
#CSHARP_FLAGS = -out:$(EXECUTABLE)
# sorce files
CSHARP_SOURCE_FILES = $(wildcard *.cs)
EXECUTABLE = $(MAIN_FILE).exe
RM_CMD = -rm $(EXECUTABLE)

all: $(EXECUTABLE)

$(EXECUTABLE): $(CSHARP_SOURCE_FILES)
	@$(CSHARP_COMPILER) $(CSHARP_SOURCE_FILES) -out:$(EXECUTABLE)
#$(CSHARP_FLAGS)

Qualifications.exe: $(CSHARP_SOURCE_FILES)
	@$(CSHARP_COMPILER) $(CSHARP_SOURCE_FILES) -out:Qualifications.exe -main:Qualifications_tests

qualifications: Qualifications.exe
	@mono Qualifications.exe

Workplace.exe: $(CSHARP_SOURCE_FILES)
	@$(CSHARP_COMPILER) $(CSHARP_SOURCE_FILES) -out:Workplace.exe -main:Workplace_tests

workplace: Workplace.exe
	@mono Workplace.exe

run: all
	@mono $(EXECUTABLE)

clean:
	@$(RM_CMD)

remake:
	@ $(MAKE) clean
	@ $(MAKE)