
#change this to the name of the Main class file, without file extension
MAIN_FILE = Main

# compiler
CSHARP_COMPILER = mcs
# compiler flags
CSHARP_FLAGS = -out:$(EXECUTABLE)
# sorce files
CSHARP_SOURCE_FILES = $(wildcard *.cs)
EXECUTABLE = $(MAIN_FILE).exe
RM_CMD = -rm $(EXECUTABLE)

all: $(EXECUTABLE)

$(EXECUTABLE): $(CSHARP_SOURCE_FILES)
	@$(CSHARP_COMPILER) $(CSHARP_SOURCE_FILES) $(CSHARP_FLAGS)

run: all
	@mono $(EXECUTABLE)

clean:
	@$(RM_CMD)

remake:
	@ $(MAKE) clean
	@ $(MAKE)