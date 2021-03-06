# CardChauffeur

1. This is a console application-type project
2. It is a console based graphical user interface
3. The entry class for this application is Program(Program.cs)

# RandomCardGenerator

1. This project is a library-type project that contains the business logic of the game.
2. State Design pattern is used
3. Please have a look at the state transistion diagram (inside Design folder) to understand the state transistions of the game before looking at the state machine code
4. The entry class for this library is RandomCardGenerator (RandomCardGenerator.cs), which load StateMachine.

# Logger

1. This project is a library-type project that contains logging and recovery management logic.
2. This library is used for logging, saving the state of the game and recovering it
3. This has a static implementation of classes 

# About the game

1. User can draw a card from a shuffled deck of 52 cards
2. User can reshuffle the deck
3. User can reset the game where it will reshuffle the deck along with the drawn card
4. User can save the state of the game and later recover it

# Features/changes to the application that are pending

1. Online multi-user functionality - multiple users can join a single session of this game
2. Session based gaming - user can save the session. This can be done now by manually storing the recovery file after saving the game
3. Undo - user can undo the last action by restoring the last state of the game
4. UI state machine - implementation of state machine for UI for more informative and interactive experience
5. Multi threading implementation for better performance
