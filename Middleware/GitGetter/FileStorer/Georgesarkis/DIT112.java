package com.example.sakog.myapplication;

import android.content.Context;
import android.support.test.InstrumentationRegistry;
import android.support.test.runner.AndroidJUnit4;

import org.junit.Test;
import org.junit.runner.RunWith;

import static org.junit.Assert.*;

/**
 * Instrumented test, which will execute on an Android device.
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
@RunWith(AndroidJUnit4.class)
public class ExampleInstrumentedTest {
    @Test
    public void useAppContext() {
        // Context of the app under test.
        Context appContext = InstrumentationRegistry.getTargetContext();

        assertEquals("com.example.sakog.myapplication", appContext.getPackageName());
    }
}

package com.example.sakog.myapplication;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.speech.SpeechRecognizer;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageButton;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.UUID;


public class MainActivity extends AppCompatActivity {
    //BLUETOOTH
    String address = null;
    private ProgressDialog progress;
    BluetoothAdapter myBluetooth = null;
    BluetoothSocket btSocket = null;
    private boolean isBtConnected = false;
    static final UUID myUUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");
    // handels the input from the arduino
    Handler handler;
    private StringBuilder sb = new StringBuilder();
    // Status  for Handler
    final int RECIEVE_MESSAGE = 1;

    private ConnectedThread mConnectedThread;


    //VOICE RECOGNITION
    /*private SpeechRecognizer mSpeechRocognizer;
    private static final String TAG = MainActivity.class.getName();
    private Handler mHandler = new Handler();
    Intent mSpeechIntent;
    private static final String[] VALID_COMMANDS = {
            "fast","default","slow","park","turn left","turn right", "forward", "backward","stop"
    };
    private static final int VALID_COMMAND_SIZE = VALID_COMMANDS.length;
    */
    //private boolean parkingStatus = false;

    //BUTTONS
    TextView textView;
    ImageButton backward ;
    ImageButton forward ;
    ImageButton right ;
    ImageButton left ;
    ImageButton volt ;
    ImageButton parkingMode ;
    ImageButton camera;
    SeekBar seekbar ;
    String command;

    @SuppressLint({"ClickableViewAccessibility", "HandlerLeak"})
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        textView = findViewById(R.id.textView);
        volt = findViewById(R.id.volt);
        backward = findViewById(R.id.down);
        forward = findViewById(R.id.forward);
        right = findViewById(R.id.rightTurn);
        left = findViewById(R.id.leftTurn);
        parkingMode = findViewById(R.id.parkingMode);
        seekbar =  findViewById(R.id.seekBar);
        camera = findViewById(R.id.camera);


        Intent newInt = getIntent();
        address = newInt.getStringExtra(bluetooth_list.EXTRA_ADDRESS); //receive the address of the bluetooth device
        new ConnectBT().execute(); //Call the class to connect

        //seekbar the number changes from 0 to 100
        seekbar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            int progressChangedValue = 0;

            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                progressChangedValue = progress;
            }

            public void onStartTrackingTouch(SeekBar seekBar) {

            }

            public void onStopTrackingTouch(SeekBar seekBar) {
                textView.setText(Integer.toString(progressChangedValue));
                if(progressChangedValue < 25){
                    sendMessage("1"); //set speed for the car on 15
                }
                else if (progressChangedValue < 50 ){
                    sendMessage("2"); //set speed for the car on 30
                }
                else if (progressChangedValue < 75 ){
                    sendMessage("3"); //set speed for the car on 45
                }
                else{
                    sendMessage("4"); //set speed for the car on 60
                }
            }
        });

        //TODO first one to try if didn't work try TODO2
        handler = new Handler() {
            public void handleMessage(android.os.Message msg) {
                switch (msg.what) {
                    case RECIEVE_MESSAGE:                                                   // if receive massage
                        byte[] readBuf = (byte[]) msg.obj;
                        String strIncom = new String(readBuf, 0, msg.arg1);                 // create string from bytes array

                        sb.append(strIncom);                                                // append string
                        int endOfLineIndex = sb.indexOf("\r\n");                            // determine the end-of-line
                        if (endOfLineIndex > 0) {                                            // if end-of-line,
                            String sbprint = sb.substring(0, endOfLineIndex);               // extract string
                            sb.delete(0, sb.length());                                      // and clear
                            Toast.makeText(getApplicationContext(),  sbprint , Toast.LENGTH_LONG).show();

                        }
                        //Log.d(TAG, "...String:"+ sb.toString() +  "Byte:" + msg.arg1 + "...");
                        break;
                }
            };
        };
        //On touch listener for the voice contral
        volt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                command = "v";
                sendMessage(command);
                //TODO this is the TODO2 look for the referencees
                //receiveData();

            }
        });

        //On touch listener for the backwarding button
        backward.setOnTouchListener(new View.OnTouchListener(){
            @Override
            public boolean onTouch(View view,MotionEvent event){
                if (event.getAction() == MotionEvent.ACTION_DOWN) { //MotionEvent.ACTION_DOWN is when you hold a button down
                    command = "b";
                    sendMessage(command);
                    }
                else if (event.getAction() == MotionEvent.ACTION_UP) { //MotionEvent.ACTION_UP is when you release a button
                    command = "s";
                    sendMessage(command);
                    }
                return false;
            }
        });
        //On touch listener for the forward button
        forward.setOnTouchListener(new View.OnTouchListener(){
            @Override
            public boolean onTouch(View view,MotionEvent event){
                if (event.getAction() == MotionEvent.ACTION_DOWN) { //MotionEvent.ACTION_DOWN is when you hold a button down
                    command = "f";
                    sendMessage(command);
                    }
                else if(event.getAction() == MotionEvent.ACTION_UP){ //MotionEvent.ACTION_UP is when you release a button
                    command = "s";
                    sendMessage(command);
                    }

                return false;
            }
        });
        //On touch listener for the turn left button
        left.setOnTouchListener(new View.OnTouchListener(){
            @Override
            public boolean onTouch(View view,MotionEvent event){
                if (event.getAction() == MotionEvent.ACTION_DOWN){ //MotionEvent.ACTION_DOWN is when you hold a button down
                    command = "l";
                    sendMessage(command);
                }
                else if(event.getAction() == MotionEvent.ACTION_UP){ //MotionEvent.ACTION_UP is when you release a button
                    command = "s";
                    sendMessage(command);
                }
                return false;
            }
        });
        //On touch listener for the turn right button
        right.setOnTouchListener(new View.OnTouchListener(){
            @Override
            public boolean onTouch(View view,MotionEvent event){
                if (event.getAction() == MotionEvent.ACTION_DOWN){ //MotionEvent.ACTION_DOWN is when you hold a button down
                    command = "r";
                    sendMessage(command);
                }
                else if(event.getAction() == MotionEvent.ACTION_UP){ //MotionEvent.ACTION_UP is when you release a button
                    command = "S";
                    sendMessage(command);
                }
                return false;
            }
        });
        parkingMode.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //if (!parkingStatus) {
                    command = "p";
                //  parkingMode.setImageResource(R.drawable.stop);
                //  parkingStatus = true;
                    sendMessage(command);
                //}
                /*else {
                    command = "m";
                    // change the logo when pressed
                    parkingMode.setImageResource(R.drawable.parking);
                    parkingStatus = false;
                    sendMessage(command);
                }*/
            }
        });
        camera.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent startIntent = new Intent(getApplicationContext(),camera_view.class);

                //TO pass information to second screen
                camera_view.getSocket(btSocket);
                startActivity(startIntent);
            }
        });



    }

    private class ConnectedThread extends Thread {
        private final InputStream mmInStream;

        public ConnectedThread(BluetoothSocket socket) {
            InputStream tmpIn = null;

            // Get the input and output streams, using temp objects because
            // member streams are final
            try {
                tmpIn = socket.getInputStream();
            } catch (IOException e) {

            }
            mmInStream = tmpIn;
        }

        public void run() {
            byte[] buffer = new byte[256];  // buffer store for the stream
            int bytes; // bytes returned from read()

            // Keep listening to the InputStream until an exception occurs
            while (true) {
                try {
                    // Read from the InputStream
                    bytes = mmInStream.read(buffer);        // Get number of bytes and message in "buffer"
                    handler.obtainMessage(RECIEVE_MESSAGE, bytes, -1, buffer).sendToTarget();     // Send to message queue Handler
                } catch (IOException e) {
                    break;
                }
            }
        }

    }
    private void sendMessage(String rMsg){
        if (btSocket!=null) {
            try {
                btSocket.getOutputStream().write(rMsg.getBytes());
            } catch (IOException e) {
                Toast.makeText(getApplicationContext(), "Error Sending Message!", Toast.LENGTH_LONG).show();
            }
        }
    }
    //TODO this is the old one didn't work
    /*private int readVolt(){
        int volt ;
        try {
           volt = btSocket.getInputStream().read();
        } catch (IOException e) {
            volt = 99999999;
        }
        return volt;
    }*/
    //TODO this is the TODO2
    /*public void receiveData() {
        byte[] buffer = new byte[1024];
        int bytes;

        while (true) {
            try {
                bytes = btSocket.getInputStream().read(buffer);

                String strReceived = new String(buffer, 0, bytes);
                Toast.makeText(getApplicationContext(), "The voltage is: " + strReceived, Toast.LENGTH_LONG).show();
            } catch (IOException e) {
                Toast.makeText(getApplicationContext(), "Could not read the inputs" , Toast.LENGTH_LONG).show();
            }
        }
    }*/

    private class ConnectBT extends AsyncTask<Void, Void, Void>  // UI thread
    {
        private boolean ConnectSuccess = true; //if it's here, it's almost connected

        @Override
        protected void onPreExecute() {
            progress = ProgressDialog.show(MainActivity.this, "Connecting...", "Please wait!!!");  //show a progress dialog
        }

        @Override
        protected Void doInBackground(Void... devices){ //while the progress dialog is shown, the connection is done in background
            try {
                if (btSocket == null || !isBtConnected) {
                    myBluetooth = BluetoothAdapter.getDefaultAdapter();//get the mobile bluetooth device
                    BluetoothDevice dispositivo = myBluetooth.getRemoteDevice(address);//connects to the device's address and checks if it's available
                    btSocket = dispositivo.createInsecureRfcommSocketToServiceRecord(myUUID);//create a RFCOMM (SPP) connection
                    BluetoothAdapter.getDefaultAdapter().cancelDiscovery();
                    btSocket.connect();//start connection

                    //TODO
                    mConnectedThread = new ConnectedThread(btSocket);
                    mConnectedThread.start();
                }
            } catch (IOException e) {
                ConnectSuccess = false;//if the try failed, you can check the exception here
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void result){ //after the doInBackground, it checks if everything went fine
            super.onPostExecute(result);

            if (!ConnectSuccess) {
                msg("Connection Failed. Is it a SPP Bluetooth? Try again.");
                finish();
            } else {
                msg("Connected.");
                isBtConnected = true;
            }
            progress.dismiss();
        }

        // fast way to call Toast
        private void msg(String s) {
            Toast.makeText(getApplicationContext(),s,Toast.LENGTH_LONG).show();
        }
    }
}

package com.example.sakog.myapplication;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Set;

public class bluetooth_list extends AppCompatActivity {
    private BluetoothAdapter myBluetooth = null;
    private Set<BluetoothDevice> pairedDevices;
    public static String EXTRA_ADDRESS = "device_address";
    ListView deviceList;
    Button refresh;

    private AdapterView.OnItemClickListener myListClickListener = new AdapterView.OnItemClickListener() {
        public void onItemClick(AdapterView<?> av, View v, int arg2, long arg3) {
            // Get the device MAC address, the last 17 chars in the View
            String info = ((TextView) v).getText().toString();
            String address = info.substring(info.length() - 17);

            // Make an intent to start next activity.
            Intent i = new Intent(bluetooth_list.this, MainActivity.class);

            //Change the activity.
            i.putExtra(EXTRA_ADDRESS, address); //this will be received at ledControl (class) Activity
            startActivity(i);
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.bluetooth_list_layout);

        //Calling widgets
        deviceList = findViewById(R.id.ListView);
        refresh = findViewById(R.id.refreshButton);
        bluetoothEnabler(); // Enable Bluetooth
        View temp = new View(getApplicationContext()); //for calling pairedDevicesList
        pairedDevicesList(temp);

        refresh.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                View temp = new View(getApplicationContext());
                pairedDevicesList(temp);
            }
        });


    }

    private void bluetoothEnabler(){
        myBluetooth = BluetoothAdapter.getDefaultAdapter();

        if (myBluetooth == null) { //if the device doesn't has bluetooth
            //Show a message that the device has no bluetooth adapter
            Toast.makeText(getApplicationContext(), "Bluetooth Device Not Available", Toast.LENGTH_LONG).show();
            finish(); //finish apk
        } else if (!myBluetooth.isEnabled()) {
            //Ask to the user turn the bluetooth on
            Intent turnBTon = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
            startActivityForResult(turnBTon, 1);
        }
    }

    public void pairedDevicesList(View view) {
        pairedDevices = myBluetooth.getBondedDevices();
        ArrayList list = new ArrayList();

        if (pairedDevices.size() > 0) {
            for (BluetoothDevice bt : pairedDevices) {
                list.add(bt.getName() + "\n" + bt.getAddress()); //Get the device's name and the address
            }
        } else {
            Toast.makeText(getApplicationContext(), "No Paired Bluetooth Devices Found.", Toast.LENGTH_LONG).show();
        }

        final ArrayAdapter adapter = new ArrayAdapter(this, android.R.layout.simple_list_item_1, list);
        deviceList.setAdapter(adapter);
        deviceList.setOnItemClickListener(myListClickListener); //Method called when the device from the list is clicked
    }
}

package com.example.sakog.myapplication;

import android.app.Activity;
import android.app.ProgressDialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothSocket;
import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Toast;


import java.io.IOException;
import java.util.UUID;


public class camera_view extends Activity {

    SensorManager sensorManager;
    Sensor sensor;

    //String address = null;
    //private ProgressDialog progress;
    //BluetoothAdapter myBluetooth = null;
    static BluetoothSocket btSocket = null;
    //private boolean isBtConnected = false;
    //static final UUID myUUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");
    private WebView webView;

    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.camera_view_layout);
        //sensor reading
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        sensor = sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE_UNCALIBRATED);


        // creating the web page and reading the url
        webView = findViewById(R.id.webView1);
        webView.getSettings().setJavaScriptEnabled(true);
        webView.loadUrl("http://192.168.0.101:8000/stream.mjpg");

        //to make it run the page in the app not in default browser
        webView.setWebViewClient(new WebViewClient(){
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url){
                view.loadUrl(url);
                return true;
            }
        });

    }


    //gyroscop readings and passing it to the arduino
    public void onResume() {
        super.onResume();
        sensorManager.registerListener(gyroListener, sensor,
                SensorManager.SENSOR_DELAY_NORMAL);
    }

    public void onStop() {
        super.onStop();
        sensorManager.unregisterListener(gyroListener);
    }

    public SensorEventListener gyroListener = new SensorEventListener() {
        public void onAccuracyChanged(Sensor sensor, int acc) { }

        public void onSensorChanged(SensorEvent event) {
            float x = event.values[0];
            float y = event.values[1];
            float z = event.values[2];
            //x = (int)x;
            y = (int)y;
            z = (int)z;
            //on portrait mode the +x goes to backward -x goes to forward
            //+z goes to left - z goes to right

            //on landscape mode the +y goes forward -y goes backward
            //+z goes left -z goes right

            //we are going to use in portrait mode
            String direction = "stop";
            if(y > 0){
                if(direction.equals("forward")){
                    sendMessage("s");
                    direction = "stop";
                }
                else{
                    sendMessage("b");
                    direction = "backward";
                }
            }
            if(y < 0){
                if(direction.equals("backward")){
                    sendMessage("s");
                    direction = "stop";
                }
                else {
                    sendMessage("f");
                    direction = "forward";
                }
            }
            if(z > 0){
                if(direction.equals("right")){
                    sendMessage("s");
                    direction = "stop";
                }
                else{
                    sendMessage("l");
                    direction = "left";
                }
            }
            if(z < 0){
                if(direction.equals("left")){
                    sendMessage("s");
                    direction = "stop";
                }
                else{
                    sendMessage("r");
                    direction = "right";
                }
            }
        }
    };


    //bluetooth connection
    public static void getSocket(BluetoothSocket Socket){
        btSocket = Socket;
    }


    //send input to arduino
    private void sendMessage(String rMsg){
        if (btSocket!=null) {
            try {
                btSocket.getOutputStream().write(rMsg.getBytes());
            } catch (IOException e) {
                Toast.makeText(getApplicationContext(), "Error Sending Message!", Toast.LENGTH_LONG).show();
            }
        }
    }

    /* reconnect to bluetooth in case it is needed
    private class ConnectBT extends AsyncTask<Void, Void, Void>  // UI thread
    {
        private boolean ConnectSuccess = true; //if it's here, it's almost connected

        @Override
        protected void onPreExecute() {
            progress = ProgressDialog.show(camera_view.this, "Connecting...", "Please wait!!!");  //show a progress dialog
        }

        @Override
        protected Void doInBackground(Void... devices){ //while the progress dialog is shown, the connection is done in background
            try {
                if (btSocket == null || !isBtConnected) {
                    myBluetooth = BluetoothAdapter.getDefaultAdapter();//get the mobile bluetooth device
                    BluetoothDevice dispositivo = myBluetooth.getRemoteDevice(address);//connects to the device's address and checks if it's available
                    btSocket = dispositivo.createInsecureRfcommSocketToServiceRecord(myUUID);//create a RFCOMM (SPP) connection
                    BluetoothAdapter.getDefaultAdapter().cancelDiscovery();
                    btSocket.connect();//start connection
                }
            } catch (IOException e) {
                ConnectSuccess = false;//if the try failed, you can check the exception here
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void result){ //after the doInBackground, it checks if everything went fine
            super.onPostExecute(result);

            if (!ConnectSuccess) {
                msg("Connection Failed. Is it a SPP Bluetooth? Try again.");
                finish();
            } else {
                msg("Connected.");
                isBtConnected = true;
            }
            progress.dismiss();
        }

        // fast way to call Toast
        private void msg(String s) {
            Toast.makeText(getApplicationContext(),s,Toast.LENGTH_LONG).show();
        }
    }*/
}
package com.example.sakog.myapplication;

import org.junit.Test;

import static org.junit.Assert.*;

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ExampleUnitTest {
    @Test
    public void addition_isCorrect() {
        assertEquals(4, 2 + 2);
    }
}
