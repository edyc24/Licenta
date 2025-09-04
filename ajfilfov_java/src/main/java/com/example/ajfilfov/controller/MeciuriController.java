package com.example.ajfilfov.controller;

import com.example.ajfilfov.entity.Meciuri;
import com.example.ajfilfov.service.MeciuriService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.*;

@RestController
@RequestMapping("/api/meciuri")
public class MeciuriController {
    private final MeciuriService service;
    public MeciuriController(MeciuriService service) { this.service = service; }

    @GetMapping
    public List<Meciuri> getAll() { return service.getAll(); }

    @GetMapping("/{id}")
    public ResponseEntity<Meciuri> getById(@PathVariable UUID id) {
        return service.getById(id).map(ResponseEntity::ok).orElse(ResponseEntity.notFound().build());
    }

    @PostMapping
    public Meciuri create(@RequestBody Meciuri meci) {
        meci.setIdMeci(UUID.randomUUID());
        if (meci.getIdDeleted() == null) meci.setIdDeleted(false);
        return service.save(meci);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable UUID id) {
        service.softDelete(id);
        return ResponseEntity.noContent().build();
    }
}
