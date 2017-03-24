// This #include statement was automatically added by the Particle IDE.
#include "FPS_GT511C3.h"
#include "SparkFunMicroOLED.h"
#include "math.h"
#include "HttpClient.h"

// ____    _            _____   _                 _                  _
//|  _ \  (_)          / ____| | |               | |                | |
//| |_) |  _    ___   | (___   | |_   _   _    __| |   ___   _ __   | |_
//|  _ <  | |  / _ \   \___ \  | __| | | | |  / _` |  / _ \ | '_ \  | __|
//| |_) | | | | (_) |  ____) | | |_  | |_| | | (_| | |  __/ | | | | | |_
//|____/  |_|  \___/  |_____/   \__|  \__,_|  \__,_|  \___| |_| |_|  \__|
//                                            Author: Ethan McMullan


FPS_GT511C3 fps;
MicroOLED oled;

bool busy = false;
int isBusy = 0;
int userID = -1;
char attend[10] = "False";
char moduleName[100] = "default";
int moduleDur = 0;
int moduleid = 0;
int attendIDs[100] = {};

HttpClient http;

http_header_t headers[] = {
    //  { "Content-Type", "[application/json"] },
    //  { "Accept" , "application/json" },
    { "Accept" , "*/*"},
    { NULL, NULL } // NOTE: Always terminate headers will NULL
};

http_request_t request;
http_response_t response;

uint8_t biostudent [] = {
  0xFC, 0x44, 0x44, 0xF8, 0x00, 0x00, 0xFC, 0xFC, 0x00, 0x00, 0xF8, 0x04, 0x04, 0xF8, 0x00, 0x00,
	0x00, 0x00, 0x80, 0x80, 0x00, 0x00, 0x00, 0x00, 0x38, 0x64, 0x44, 0xD8, 0x00, 0x00, 0x04, 0xFC,
	0xFC, 0x04, 0x00, 0x00, 0xFC, 0x00, 0x00, 0xFC, 0x00, 0x00, 0xFC, 0xFC, 0x04, 0x04, 0xF8, 0x00,
	0x00, 0xFC, 0xFC, 0x44, 0x00, 0x00, 0xFC, 0x70, 0x40, 0xFC, 0xFC, 0x00, 0x04, 0xFC, 0xFC, 0x04,
	0x07, 0x04, 0x04, 0x07, 0x01, 0x00, 0x07, 0x07, 0x00, 0x00, 0x03, 0x04, 0x04, 0x03, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x83, 0x04, 0x44, 0x43, 0x40, 0x00, 0x20, 0x27,
	0x27, 0x20, 0x00, 0x40, 0x43, 0x44, 0x44, 0x83, 0x80, 0x00, 0x07, 0x07, 0x04, 0x04, 0x03, 0x00,
	0x00, 0x07, 0x07, 0x04, 0x00, 0x00, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0xC0, 0x00, 0x08, 0x84, 0x42, 0x22, 0x11, 0x88, 0xC4, 0x04, 0x02, 0x12, 0x92, 0x90, 0x90, 0x80,
	0x80, 0x90, 0x92, 0x12, 0x12, 0x22, 0x40, 0x40, 0x88, 0x11, 0x22, 0x42, 0x84, 0x08, 0x30, 0xC0,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x07,
	0x00, 0x00, 0x0E, 0x01, 0x00, 0x38, 0x06, 0x01, 0xF8, 0x04, 0x02, 0x01, 0x00, 0x18, 0x04, 0x04,
	0xC4, 0x04, 0x08, 0xF9, 0x01, 0x06, 0x18, 0x00, 0x01, 0x06, 0xF8, 0x00, 0x01, 0xFE, 0x00, 0x00,
	0x07, 0xF8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04,
	0x24, 0x23, 0x20, 0x10, 0x88, 0x47, 0x60, 0x18, 0x07, 0x00, 0x80, 0x60, 0x1C, 0x00, 0x80, 0x70,
	0x0F, 0x00, 0x80, 0x01, 0x00, 0x00, 0xF8, 0x01, 0x00, 0xC0, 0x3F, 0x00, 0x00, 0xFF, 0x00, 0x00,
	0xC0, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x01, 0x01, 0x01, 0x08, 0x00, 0x04, 0x04, 0x22, 0x21, 0x10, 0x08, 0x08, 0x00, 0x41, 0x20,
	0x18, 0x06, 0x41, 0x20, 0x18, 0x07, 0x00, 0x30, 0x0C, 0x03, 0x00, 0x08, 0x07, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
};

void setup() {
	Serial.begin(9600);
	delay(100);
  oled.begin();
	oled.clear(ALL);
	oled.drawBitmap(biostudent);
	oled.display();
	fps.Open();
	fps.SetLED(false);
  Particle.function("identify", identifyUser);
  Particle.function("enroll", enrollUser);
  Particle.variable("isbusy", &isBusy, INT);
  Particle.variable("userid", &userID, INT);
  Particle.variable("attendance", attend, STRING);
  Particle.variable("modulename", moduleName, STRING);
  Particle.variable("moduledur", &moduleDur, INT);
  Particle.variable("moduleid", &moduleid, INT);
  Particle.function("setatn", sAtn);
  Particle.function("sModuleName", setModName);
  Particle.function("sModDuration", setModDur);
  Particle.function("sModID", setModID);
}

size_t strlcpy(char *dst, const char *src, size_t size)
{
    size_t len = 0;
    while(size > 1 && *src)
    {
        *dst++ = *src++;
        size--;
        len++;
    }
    if(size > 0)
        *dst = 0;
    return len + strlen(src);
}

int sAtn(String attribute)
{
  strlcpy(attend, attribute, 10);
  Serial.print("ATTEND: ");
  Serial.println(attend);
  Serial.println(attribute);
  if(attribute == "True")
  {
    studentAttendance();
  }else{
    isBusy = 0;
    busy = false;
    fps.SetLED(false);
  }
  return 1;
}

int setModName(String attribute)
{
  strlcpy(moduleName, attribute, 100);
  return 1;
}

boolean arrayIncludeElement(int array[], int element) {
for (int i = 0; i < sizeof(array); i++) {
     if (array[i] == element) {
         return true;
     }
   }
 return false;
}

int setModDur(String attribute)
{
  moduleDur = attribute.toInt();
  return 1;
}

int setModID(String attribute)
{
  moduleid = attribute.toInt();
  return 1;
}

void studentAttendance()
{
  busy = !busy;
  isBusy = 1;
  Serial.print(busy);

  while(busy == true)
  {
    fps.SetLED(true);
    Particle.unsubscribe();
    if(fps.IsPressFinger())
    {
        fps.CaptureFinger(false);
        int id = fps.Identify1_N();
        if (id < 200)
        {
            userID = id;
            Serial.print("Verified Fingerprint! ID:");
            Serial.println(id);
            oled.clear(PAGE);
            oled.setCursor(0,0);
            oled.setFontType(1);
            oled.print("Thank You!");
            attendIDs[sizeof(attendIDs) - 1] = userID;
            for(int i = 0; i < sizeof(attendIDs); i++){
              Serial.println("IDS in array: " + String(attendIDs[i]));
            }
            oled.display();
            //Particle.publish("sattend", String(userID));
            request.hostname = "138.68.171.90";
            request.port = 80;
            request.path = "/attendance.php";
            //request.body = "{\"key\":\"value\"}"
            //request.body = c;
            String ou = "{\"userID\":\"";
            ou.concat(String(userID) + "\",");
            ou.concat("\"moduleID\":\"" + String(moduleid) + "\"}");
            Serial.println(ou);
            request.body = ou;
            // Get request
            http.get(request, response, headers);
            //http.post(request, response, headers);
            Serial.print("Application>\tResponse status: ");
            Serial.println(response.status);

            Serial.print("Application>\tHTTP Response Body: ");
            Serial.println(response.body);
            fps.SetLED(false);
            delay(3000);
            oled.clear(PAGE);
            oled.drawBitmap(biostudent);
            oled.display();
          }else{
            Serial.println("Fingerprint not found!");
            fps.SetLED(false);
            oled.clear(PAGE);
            oled.setCursor(0,0);
            oled.setFontType(1);
            oled.print("Error!");
            userID = -1;
            oled.display();
            delay(3000);
            oled.clear(PAGE);
            oled.drawBitmap(biostudent);
            oled.display();
          }
        }
  }
}

int identifyUser(String command)
{
  busy = !busy;
  isBusy = 1;
  userID = -1;

    while(busy == true)
    {
      Particle.unsubscribe();
      fps.SetLED(true);
      oled.clear(PAGE);
      oled.setCursor(0,0);
      oled.setFontType(0);
      oled.print("Place     finger    on sensor!");
      if(fps.IsPressFinger())
      {
          fps.CaptureFinger(false);
          int id = fps.Identify1_N();
          if (id < 200)
          {
              userID = id;
              Serial.print("Verified Fingerprint! ID:");
			        Serial.println(id);
              oled.clear(PAGE);
              oled.setCursor(0,0);
              oled.setFontType(1);
              oled.print("ID:");
              oled.setCursor(30,0);
              oled.print(id);
              oled.display();
              Particle.publish("user_identify", "success");
              fps.SetLED(false);
              delay(3000);
              oled.clear(PAGE);
            	oled.drawBitmap(biostudent);
            	oled.display();
              isBusy = 0;
              busy = !busy;
              return 0;
              break;
          }else{
              Serial.println("Fingerprint not found!");
              fps.SetLED(false);
              oled.clear(PAGE);
              oled.setCursor(0,0);
              oled.setFontType(1);
              oled.print("NO ID!");
              userID = -1;
              oled.display();
              delay(3000);
              oled.clear(PAGE);
            	oled.drawBitmap(biostudent);
            	oled.display();
              isBusy = 0;
              busy = !busy;
              return 0;
              break;
          }
      }
    }
}

int enrollUser(String command)
{
  busy = !busy;
  isBusy = 1;
  userID = -1;
  Serial.print(busy);

  while(busy == true)
  {
    Particle.unsubscribe();
    int enrollid = 0;
  	bool usedid = true;
  	while (usedid == true)
  	{
  		usedid = fps.CheckEnrolled(enrollid);
  		if (usedid==true) enrollid++;
  	}
    fps.EnrollStart(enrollid);
  	fps.SetLED(true);
  	// enroll
  	Serial.print("Press finger to Enroll #");
  	Serial.println(enrollid);
  	oled.clear(PAGE);
  	oled.setCursor(0,0);
  	oled.setFontType(0);
  	oled.print("Place     finger    on sensor!");
  	oled.display();
  	while(fps.IsPressFinger() == false) delay(100);
  	bool bret = fps.CaptureFinger(true);
  	int iret = 0;
  	if (bret != false)
  	{
  		fps.SetLED(false);
  		oled.clear(PAGE);
  		oled.setCursor(0,0);
  		oled.setFontType(0);
  		oled.print("Remove    finger!");
  		oled.display();
  		fps.SetLED(true);
  		fps.Enroll1();
  		while(fps.IsPressFinger() == true) delay(100);
  		oled.clear(PAGE);
  		oled.setCursor(0,0);
  		oled.setFontType(0);
  		oled.print("Place     finger    on sensor!");
  		oled.display();
  		while(fps.IsPressFinger() == false) delay(100);
  		bret = fps.CaptureFinger(true);
  		if (bret != false)
  		{
  			oled.clear(PAGE);
  			oled.setCursor(0,0);
  			oled.setFontType(0);
  			fps.SetLED(false);
  			oled.print("Remove    finger!");
  			oled.display();
  			fps.Enroll2();
  			while(fps.IsPressFinger() == true) delay(100);
  			oled.clear(PAGE);
  			fps.SetLED(true);
  			oled.setCursor(0,0);
  			oled.setFontType(0);
  			oled.print("Place     finger    on sensor!");
  			oled.display();
  			while(fps.IsPressFinger() == false) delay(100);
  			bret = fps.CaptureFinger(true);
  			if (bret != false)
  			{
  				oled.clear(PAGE);
  				oled.setCursor(0,0);
  				oled.setFontType(0);
  				fps.SetLED(false);
  				oled.print("Remove    finger!");
  				oled.display();
  				iret = fps.Enroll3();
  				if (iret == 0)
  				{
  					oled.clear(PAGE);
  					oled.setCursor(0,0);
  					oled.setFontType(0);
  					fps.SetLED(false);
  					oled.print("Enrolled!");
  					oled.display();
            userID = enrollid;
            Particle.publish("user_enroll", "success");
            isBusy = 0;
            busy = !busy;
  					delay(4000);
  					displayTitleScreen();
            return 1;
            break;
  				}
  				else
  				{
  					oled.clear(PAGE);
  					oled.setCursor(0,0);
  					oled.setFontType(0);
  					fps.SetLED(false);
  					oled.print("ERROR:");
  					oled.setCursor(0,20);
  					oled.print(iret);
  					oled.display();
            isBusy = 0;
            busy = !busy;
            userID = -1;
  					delay(4000);
  					displayTitleScreen();
            return 0;
            break;
  				}
  			}
  			else enrollError();
  		}
  		else enrollError();
  	}
  	else enrollError();
  }
}

int enrollError()
{
	oled.clear(PAGE);
	oled.setCursor(0,0);
	oled.setFontType(0);
	oled.print("ERROR!");
	oled.display();
	fps.SetLED(false);
  isBusy = 0;
  busy = !busy;
  userID = -1;
	delay(4000);
	displayTitleScreen();
  return 0;
}

void displayTitleScreen(){
	oled.clear(PAGE);
	oled.drawBitmap(biostudent);
	oled.display();
}
