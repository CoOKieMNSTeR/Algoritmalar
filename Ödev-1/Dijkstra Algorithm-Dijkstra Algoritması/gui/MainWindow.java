package gui;

import algo.DijkstraAlgorithm;
import models.Graph;

import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.border.EmptyBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

public class MainWindow extends JPanel {

    private Graph graph;
    private GraphPanel graphPanel;

    public MainWindow(){
        super.setLayout(new BorderLayout());
        setGraphPanel();
    }

    private void setGraphPanel(){
        graph = new Graph();
        graphPanel = new GraphPanel(graph);
        graphPanel.setPreferredSize(new Dimension(9000, 4096));

        JScrollPane scroll = new JScrollPane();
        scroll.setViewportView(graphPanel);
        scroll.setPreferredSize(new Dimension(750, 500));
        scroll.getViewport().setViewPosition(new Point(4100, 0));
        add(scroll, BorderLayout.CENTER);
        setTopPanel();
        setButtons();
    }

    private void setTopPanel() {
        JLabel info = new JLabel("Dijkstra Shortest Path(Dijkstra Kýsa Yol Algoritmasý)--"
        		+ "Muhammed Said BAKIRCI");
        info.setForeground(new Color(230, 220, 250));
        JPanel panel = new JPanel();
        panel.setBackground(new Color(130, 50, 250));
        panel.add(info);
        panel.setBorder(new EmptyBorder(5, 5, 5, 5));
        add(panel, BorderLayout.NORTH);
    }

    private void setButtons(){
        JButton run = new JButton();
        setupIcon(run, "run");
        JButton reset = new JButton();
        setupIcon(reset, "reset");
        final JButton info = new JButton();
        setupIcon(info, "info");

        JPanel buttonPanel = new JPanel();
        buttonPanel.setBackground(DrawUtils.parseColor("#DDDDDD"));
        buttonPanel.add(reset);
        buttonPanel.add(run);
        buttonPanel.add(info);

        reset.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                graphPanel.reset();
            }
        });

        info.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                JOptionPane.showMessageDialog(null,
                        "Yeni düðüm oluþturmak için boþ alana týklayýn\n" +
                        "Bir kenar oluþturmak için düðümlerden düðüme sürükleyin\n\n" +
                        "Kombinasyonlar:\n" +
                        "Shift + Left Click       :    Düðümü kaynak olarak ayarlama\n" +
                        "Shift + Right Click     :    Düðümü hedef olarak ayarlama\n" +
                        "Ctrl  + Drag               :  Düðümü  Yeniden Konumlandýrma \n" +
                        "Ctrl  + Click                :    Düðüm Yolu Al\n" +
                        "Ctrl  + Shift + Click   :    Düðümü / Kenarý Sil\n");
            }
        });

        run.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(graph);
                try{
                    dijkstraAlgorithm.run();
                    graphPanel.setPath(dijkstraAlgorithm.getDestinationPath());
                } catch (IllegalStateException ise){
                    JOptionPane.showMessageDialog(null, ise.getMessage());
                }
            }
        });

        add(buttonPanel, BorderLayout.SOUTH);
    }

    private void setupIcon(JButton button, String img){
        try {
            Image icon = ImageIO.read(getClass().getResource(
                    "/resources/" + img + ".png"));
            ImageIcon imageIcon = new ImageIcon(icon);
            button.setIcon(imageIcon);
            button.setBorderPainted(false);
            button.setFocusPainted(false);
            button.setContentAreaFilled(false);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
