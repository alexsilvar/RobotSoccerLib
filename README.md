# RobotSoccerLib
Generic Architecture for Robot Soccer Very Small Size

# Motivation
The Robot Soccer category Very Small Size is a sport which involves several areas of computational knoledge. Coordinate the flow of the information mantaining the consistensy and flexibility is hard to do.
This library make the flow logic and leaves the important parts implementation to the developer (Computer Vision, Strategy and Wireless Comunication). Also provides a sample working implementation. To use it you must add to your project the dependencies.

# Dependencies
- EmguCV

# Game Parts
It's process which occur in Software can be segregated in tree diferent areas, Computer Vision, Strategy and wireless Comunication.
Between each of them flows the information of the game.

## Overview
The system can be overviewed in the image, the messages (yellow) which floats through the actuators(blue).

<img src="https://raw.githubusercontent.com/alexsilvar/RobotSoccerLib/master/images/actAndMsgs.png"  width="600" height="400">

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

The **Control** is a class wich use generic types to make it adaptable without need to change directly it's code.
Once defined, the parameters adapt itself to receive the correct object types.

The **Generic Types** defined are:
- **Img:** The image Type to go to the Computer Vision Component
- **VtoERobo:** Object containing the robot **Key-Points** to be used in Strategy Component
- **EtoCRobo:** Object containing the **Decision** to be used in Wireless Comunication Component
- **VtoEBola:** Object containing the ball **Key-Points** be used in Strategy Component
- **VtoECampo:** Object containing the field **Key-Points** to be used in Strategy Component
- **PlaceToDraw:** Type of place to draw the processed image

## Control Constructor

The Controle constructor is where the video capturer is provided:

```
public Controle(IVideoRetriever<Img, PlaceToDraw> capturaVideo)
```

## Used Interfaces

The interfaces share the generic types with the **Control** class. It occurs because they are parameters used to execute the process inside it. The interfaces and they're respective Generic Types are:

| Interface | Generic Type |
| :----: | :----: | 
| **IVideoCaptura** | Img, PlaceToDraw |
| **IVisao**        | Img, VtoERobo, PlaceToDraw |
| **IEstrategia**   | VtoERobo, EtoCRobo, VtoEBola, VtoECampo |
| **IComunicacao**  | EtoCRobo |

The **IVideoCaptura** is a segregation of the video capture and image processing. In this way the image to be analized have only one source responsible to provide the image from the enviorment or any other source desired. It also draw the original image into a PlaceToDraw reference.
This component raises an Event every time it retrieves an image. Here can be handle, for example, the 

```
public interface IVideoCaptura<Img,PlaceToDraw>
public interface IVisao<Img, VtoERobo, PlaceToDraw>
public interface IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo>
public interface IComunicacao<EtoCRobo>
```

# The Pilot Class

The **Pilot** is the class where the **Control** generic types are defined specifically. Here is instantiated a Control object.

Alone the Pilot cannot do anything and the Control cannot work without the Pilot.

A pilot need to be developed after define it's dependencies. Thease dependencies can be any kind of object. 
For example the generic **PlaceToDraw** can be a PictureBox from System.Drawing library, and **VtoERobo** a custom developed class or struct.

## Setting up the Control

Now the pilot must turn on the engine (get ready to fly... soccer). So a Control member variable must be created using the desired types.
```
Controle<MyImg, MyVtoERobo, MyEtoCRobo, MyVtoEBola, MyVtoECampo, MyPlaceToDraw> controle;
```

The "My" Types are defined by the Pilot **Developer**. It must be created providing the information type and 


# Sample Pilot Implementation - SimpleVSSS
This implementation is an example of how it is easy to create a **Pilot** wich does the connection between UI and the process.
This Pilot comes with some sample implementation and can be setted up like:

```
public void setupCampo(Dictionary<int, Rectangle> paramCampo, ref PictureBox placeToDraw)
public void setupBola(Range paramBola, ref PictureBox placeToDraw)
public void novoRoboBasico(string papel, Range rangeRobo, Range rangeTime, string portaCom, ref PictureBox placeToDraw)
public void controleManual(string id, int velRodaD, int velRodaE)
public void novoCustomRobo(string id, 
            IVisao<Bitmap, InfoVtoERobo, PictureBox> vRobo,
            IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo> eRobo,
            IComunicacao<InfoEtoCRobo> cRobo,
            ref PictureBox placeToDraw)
```

The sample implementation defined as **SimpleVSSS** creates a sample **Control** object usig the following specific types.

| Generic         | Specific      | Dependency           |
| :-------------: | :-----------: | :------------------: |
| **Img**         | Bitmap        | System.Drawing       |
| **VtoERobo**    | InfoVtoERobo  | RobotSoccerLib       |
| **EtoCRobo**    | InfoEtoCRobo  | RobotSoccerLib       |
| **VtoEBola**    | InfoVtoEBola  | RobotSoccerLib       |
| **VtoECampo**   | InfoVtoECampo | RobotSoccerLib       |
| **PlaceToDraw** | PictureBox    | System.Windows.Forms |

# Using the SimpleVSSS

To use the Simple VSSS you must create your UI and provide the correct parameters to, for example, show the captured and processed images or manually control a robot.

## Setup SimpleVSSS

To default setup the enviorment you must use the constructor passing the camId (Integer number, 0 in most of cases). 

Now you have two options, use the basic setup using the constrains provided (see wiki in the future for futher information) or the custom setup providing the preconfigured Objects wich implements the parameters interfaces. The first does my implementation of IVideoCaptura, IVisao, IEstrategia and IComunicacao. The second you use your own implementation and they can be combined( used together)

### Default Use

--- Construction---

### Custom Use

---Construction---










