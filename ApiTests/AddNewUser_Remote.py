import requests
import json
from datetime import datetime

ip = "145.44.233.207"
port = "80"
protocol = "https"
ageSecs = 567648000

def log (x):
    print (x)

def tpass (x):
    x = True
    print ("pass: " + str(x))

def tfail (x):
    x = False
    n = 1
    for test in tests:
        print (test[n])
        n += 1

    exit ()

tests = {
    1: {
        1: False,
        2: False,
        3: False,
        4: False,
        5: False,
        6: False,
        7: False
    },
    2: {
        1: False
    }
}

# # # FIRST TEST # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

# The first test is to add a user to the users database.

# Steps:
#   1.  Prepare a NewUserData object to serialize.
#           A NewUserData is represented in json as follows:
#               {"email":"<string>","password":"<string>","salt":"<string>","realName":"<string>","birthdate":<long>}
#           A json string of this format will be deserializable to a NewUserData object.

payload = {
    "email": "unit@test.net",
    "password": "unittest_password",
    "salt": "unittest_salt",
    "realName": "Unit Test",
    "birthdate": datetime.timestamp(datetime.now()) - ageSecs,
    "id": 0,
    "coverImage": "1.jpg"
}

if payload is None:
    # tfail (tests[1][1])
    print ("failed 1.1")

#   2.  Log the current contents of the userMap.json file.
#           This can be done by requesting <protocol>://<ip>/user/get/all.
#           The result will be a dictionary of <email>:<userid> pairs

r = requests.get(protocol + "://" + ip + "/user/get/all", verify=False)
j = json.loads(r.text)

if j is None:
    tfail (tests[1][2])

if r.status_code < 400 :
    # tpass (tests[1][2])
    print ("passed 1.2")
else:
    # tfail (tests[1][2])
    print ("failed 1.2")

for k in j:
    log (k)

#   3.  Confirm that the user does not exist.
#           If the user's email is not a key in the userMap.json file,
#           it does not exist.

if payload["email"] in j:
    log ("email already exists")
    # tfail (tests[1][3])
    print ("failed 1.3")
else:
    log ("email does not exist")
    # tpass (tests[1][3])
    print ("passed 1.3")

#   4.  Send the data to the server in a post request.

r = requests.post("https://145.44.233.207/user/post/new", json=payload, verify=False)

if r.status_code < 400 :
    # tpass (tests[1][4])
    print ("passed 1.4")
else:
    # tfail (tests[1][4])
    print ("failed 1.4, status: " + str(r.status_code))

#   5.  Retrieve the userMap

r = requests.get(protocol + "://" + ip + "/user/get/all", verify=False)
j = json.loads(r.text)

if j is None:
    # tfail (tests[1][5])
    print ("failed 1.5")

if r.status_code < 400 :
    # tpass (tests[1][5])
    print ("passed 1.5")
else:
    # tfail (tests[1][5])
    print ("failed 1.5")

for k in j:
    log (k)

#   6.  Confirm that the user has been added to the userMap

if payload["email"] in j:
    log ("email already exists")
    # tpass (tests[1][6])
    print ("passed 1.6")
else:
    log ("email does not exist")
    # tfail (tests[1][6])
    print ("failed 1.6")

#   7.  Confirm that the server's user data matches the data we sent

r = requests.get(protocol + "://" + ip + "/user/get/email=" + payload["email"], verify=False)
j = json.loads(r.text)

if j is None:
    # tfail (tests[1][7])
    print ("failed 1.7")

if r.status_code >= 400 :
    tfail (tests[1][7])

for k in j:
    if k in payload:
        if j[k] is not payload[k]:
            # tfail (tests[1][7])
            print ("failed 1.7")

# tpass (tests[1][7])
print ("passed 1.7")

exit ()
