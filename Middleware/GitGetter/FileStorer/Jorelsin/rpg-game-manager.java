import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;

public class GameFrame {

    String CampaingName;
    JFrame frame;
    JPanel panel;
    JsonReader reader;
    JFileChooser fileChooser;

    GameFrame(String name) {
        CampaingName = getChooser();
        this.frame = new JFrame(name);
        panel = new JPanel(null);
        reader = new JsonReader(CampaingName);
        setGameFrame(reader.getAppSize());
    }

    public void setGameFrame(int size[]){
        frame.setSize(size[0],size[1]);
        frame.add(panel);
        setPanel();
        frame.setVisible(true);
    }

    public JLabel getCampaingName() {
        String CName = reader.getCName();
        JLabel Name = new JLabel(CName);
        Name.setBounds(8, 35, 125, 30);
        Name.setFont(new Font("Arial", Font.BOLD,15));
        Name.setForeground(Color.white);

        return Name;
    }

    public void setPanel(){
        panel.setBounds(0,0,150,400);
        panel.setBackground(Color.DARK_GRAY);
        panel.add(getCampaingName());
    }

    public String getChooser(){
        String json ="";
        fileChooser = new JFileChooser("Campaigns");
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Campaings", "json");
        fileChooser.setFileFilter(filter);
        int returnVal = fileChooser.showOpenDialog(panel);
        if(returnVal == JFileChooser.APPROVE_OPTION){
          return json = fileChooser.getSelectedFile().getPath();
        }
        if(json == ""){
           json = getChooser();
        }
        return json;
    }
}
 import org.json.JSONObject;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

public class JsonReader {

    String Cname;

    JsonReader(String Cname){
        this.Cname = Cname;
    }

    public String getCName(){
        String jsonText;
        JSONObject reader;
        String name = null;
        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            reader = new JSONObject(jsonText.trim());
            name = reader.getJSONObject("CampaingInfo").getString("Name");
            return name;

        }catch (IOException e){
            e.printStackTrace();
        }
        if (name == null){
            name = "ERROR!";
        }
        return name;
    }

    public int[] getAppSize(){
        String jsonText;
        JSONObject parser;
        int w = 1080;
        int h = 780;
        int size[] = new int [2];
        size[0] = w;
        size[1] = h;

        try{
            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File(Cname)));
            parser = new JSONObject(jsonText.trim());
            w = parser.getJSONObject("AppInfo").getInt("appW");
            h = parser.getJSONObject("AppInfo").getInt("appH");
            size[0] = w;
            size[1] = h;
            return size;

        }catch (IOException e){
            e.printStackTrace();
        }

        return size;
    }

}
 import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;

public class JsonWriter {
    FileWriter file;
    String Cname;

    JsonWriter(String Cname){
        this.Cname = Cname;
    }

    public void setCName(String name) throws IOException{

        String jsonText;
        JSONObject writer;


            jsonText = org.apache.commons.io.IOUtils.toString(new FileInputStream(new File("Campaigns/Oikos/Oikos")));
            writer = new JSONObject(jsonText.trim());
            JSONObject info = writer.getJSONObject("CampaingInfo");
            info.put("Name",name);
            file = new FileWriter("Campaigns/Oikos/Oikos");
            file.write(writer.toString());
            file.flush();
            file.close();

    }
}
 import javax.swing.*;
import java.awt.*;
import java.io.IOException;

public class MainWindow {
    public static void main(String[] args) throws IOException, ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        String appName = "Jorelsin´s Game Manager";
        GameFrame mainwindow = new GameFrame(appName);
    }

}
 