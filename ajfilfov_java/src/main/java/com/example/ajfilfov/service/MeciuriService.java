package com.example.ajfilfov.service;

import com.example.ajfilfov.entity.Meciuri;
import com.example.ajfilfov.repository.MeciuriRepository;
import org.springframework.stereotype.Service;
import java.util.*;

@Service
public class MeciuriService {
    private final MeciuriRepository repo;
    public MeciuriService(MeciuriRepository repo) { this.repo = repo; }
    public List<Meciuri> getAll() {
        try { return repo.findByIdDeletedFalse(); }
        catch (Exception e) { return repo.findAll(); }
    }
    public Optional<Meciuri> getById(UUID id) { return repo.findById(id); }
    public Meciuri save(Meciuri meci) { return repo.save(meci); }
    public void softDelete(UUID id) { repo.findById(id).ifPresent(m -> { m.setIdDeleted(true); repo.save(m); }); }
}
