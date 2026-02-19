package com.example.ajfilfov.entity;

import jakarta.persistence.*;
import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Table(name = "Meciuri")
public class Meciuri {
    @Id
    @Column(name = "IdMeci", nullable = false)
    private UUID idMeci;
    @Column(name = "IdEchipaGazda")
    private UUID idEchipaGazda;
    @Column(name = "IdEchipaOaspete")
    private UUID idEchipaOaspete;
    @Column(name = "DataJoc")
    private LocalDateTime dataJoc;
    @Column(name = "Rezultat")
    private String rezultat;
    @Column(name = "Observatii")
    private String observatii;
    @Lob
    @Column(name = "Raport")
    private byte[] raport;
    @Column(name = "IdArbitru")
    private UUID idArbitru;
    @Column(name = "IdArbitruAsistent1")
    private UUID idArbitruAsistent1;
    @Column(name = "IdArbitruAsistent2")
    private UUID idArbitruAsistent2;
    @Column(name = "IdArbitruRezerva")
    private UUID idArbitruRezerva;
    @Column(name = "IdObservator")
    private UUID idObservator;
    @Column(name = "IdStadionLocalitate")
    private UUID idStadionLocalitate;
    @Column(name = "ScorGazde")
    private Integer scorGazde;
    @Column(name = "ScorOaspeti")
    private Integer scorOaspeti;
    @Column(name = "Locatie")
    private String locatie;
    @Column(name = "Etapa")
    private Integer etapa;
    @Column(name = "IdDeleted")
    private Boolean idDeleted;

    public UUID getIdMeci() { return idMeci; }
    public void setIdMeci(UUID idMeci) { this.idMeci = idMeci; }
    public UUID getIdEchipaGazda() { return idEchipaGazda; }
    public void setIdEchipaGazda(UUID idEchipaGazda) { this.idEchipaGazda = idEchipaGazda; }
    public UUID getIdEchipaOaspete() { return idEchipaOaspete; }
    public void setIdEchipaOaspete(UUID idEchipaOaspete) { this.idEchipaOaspete = idEchipaOaspete; }
    public LocalDateTime getDataJoc() { return dataJoc; }
    public void setDataJoc(LocalDateTime dataJoc) { this.dataJoc = dataJoc; }
    public String getRezultat() { return rezultat; }
    public void setRezultat(String rezultat) { this.rezultat = rezultat; }
    public String getObservatii() { return observatii; }
    public void setObservatii(String observatii) { this.observatii = observatii; }
    public byte[] getRaport() { return raport; }
    public void setRaport(byte[] raport) { this.raport = raport; }
    public UUID getIdArbitru() { return idArbitru; }
    public void setIdArbitru(UUID idArbitru) { this.idArbitru = idArbitru; }
    public UUID getIdArbitruAsistent1() { return idArbitruAsistent1; }
    public void setIdArbitruAsistent1(UUID idArbitruAsistent1) { this.idArbitruAsistent1 = idArbitruAsistent1; }
    public UUID getIdArbitruAsistent2() { return idArbitruAsistent2; }
    public void setIdArbitruAsistent2(UUID idArbitruAsistent2) { this.idArbitruAsistent2 = idArbitruAsistent2; }
    public UUID getIdArbitruRezerva() { return idArbitruRezerva; }
    public void setIdArbitruRezerva(UUID idArbitruRezerva) { this.idArbitruRezerva = idArbitruRezerva; }
    public UUID getIdObservator() { return idObservator; }
    public void setIdObservator(UUID idObservator) { this.idObservator = idObservator; }
    public UUID getIdStadionLocalitate() { return idStadionLocalitate; }
    public void setIdStadionLocalitate(UUID idStadionLocalitate) { this.idStadionLocalitate = idStadionLocalitate; }
    public Integer getScorGazde() { return scorGazde; }
    public void setScorGazde(Integer scorGazde) { this.scorGazde = scorGazde; }
    public Integer getScorOaspeti() { return scorOaspeti; }
    public void setScorOaspeti(Integer scorOaspeti) { this.scorOaspeti = scorOaspeti; }
    public String getLocatie() { return locatie; }
    public void setLocatie(String locatie) { this.locatie = locatie; }
    public Integer getEtapa() { return etapa; }
    public void setEtapa(Integer etapa) { this.etapa = etapa; }
    public Boolean getIdDeleted() { return idDeleted; }
    public void setIdDeleted(Boolean idDeleted) { this.idDeleted = idDeleted; }
}
