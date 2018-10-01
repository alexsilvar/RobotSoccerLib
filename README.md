# RobotSoccerLib
Generic Architecture for Robot Soccer Very Small Size

# Motivation
The Robot Soccer category Very Small Size is a sport which involves several areas of computational knoledge. Coordinate the flow of the information mantaining the consistensy and flexibility is hard to do.
This library make the flow logic and leaves the important parts implementation to the developer. Also provides a sample working implementation. To use it you must add to your project the dependencies.

# Dependencies
- EmguCV

# Game Parts
It's process wich occur in Software can be segregated in tree diferent areas, Computer Vision, Strategy and wireless Comunication.
Between each of them flows the information of the game.

## Computer Vision

This part is responsible for processing the images and defining points wich goes to the strategy. Here comes the most expensive part of the process.

### Input
Image captured from the enviorment.

### Output
Key-points understanded by the Strategy.

## Strategy

Here we have the decision making. This part analizes the points informed by the Computer Vision and define wich information must be send to the robots.

### Input
Key-points from Computer Vision.

### Output
Decisions to send to the robots.

## Wireless Comunication

In the Wireless Comunication the message from strategy is encoded for the robot understanding then send using the desired Wifi technology.

### Input
Processed information from Strategy (usualy speed of the weels, angulation of movement etc.)

### Output
Protocoled info dispatched to the robots.


# Information flow

The information floats through each part, being transformed then go to the next one until reach the robot.
The information flow and it's content can be described as bellow:
- Information for Robots
  - Captured **Image** to Computer Vision
  - **Key-Points** defined by Computer Vision to Strategy
  - **Decision** for the robots movement from Strategy to Wireless Comunication
- Information for Ball and Field
  - Captured **Image** to Computer Vision
  - **Key-Points** defined by Computer Vision to Strategy

# Why Generic

The scalability is a always wanted thing in software development. With generic types the information can change but the ''backend'' flow still the same.


# Control Behavior

The control is a class wich

- **Img:** The image Type to go to the Computer Vision Component
- **VtoERobo:** Object containing the robot **Key-Points** to be used in Strategy Component
- **EtoCRobo:** Object containing the **Decision** to be used in Wireless Comunication Component
- **VtoEBola:** Object containing the ball **Key-Points** be used in Strategy Component
- **VtoECampo:** Object containing the field **Key-Points** to be used in Strategy Component
- **PlaceToDraw:** Type of place to draw the processed image

The control can do the following things

-- Construction --

# Sample Implementation
The sample implementation defined as **SimpleVSSS** creates a sample **Control** object usig not generic types.

| Generic         | Specific      |
| --------------- | ------------- |
| **Img**         | Bitmap        |
| **VtoERobo**    | InfoVtoERobo  |
| **EtoCRobo**    | InfoEtoCRobo  |
| **VtoEBola**    | InfoVtoEBola  |
| **VtoECampo**   | InfoVtoECampo |
| **PlaceToDraw** | PictureBox    |












