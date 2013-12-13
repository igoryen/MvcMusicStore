﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers {
  public class StoreManagerController : Controller {
    private MusicStoreEntities db = new MusicStoreEntities();

    //
    // GET: /StoreManager/

    public ActionResult Index() {
      var albums = db.Albums.Include(a => a.Genre).Include(a => a.Artist); // 10
      return View(albums.ToList()); // 11
    }

    //
    // GET: /StoreManager/Details/5

    public ActionResult Details(int id = 0) {
      Album album = db.Albums.Find(id); // 20
      if (album == null) {
        return HttpNotFound();
      }
      return View(album); // 24 
    }

    //
    // GET: /StoreManager/Create

    public ActionResult Create() { // 30
      ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name"); // 31
      ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name"); // 32
      return View();
    }

    //
    // POST: /StoreManager/Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Album album) { // 43
      if (ModelState.IsValid) { // 44
        db.Albums.Add(album); // 45
        db.SaveChanges(); // 46
        return RedirectToAction("Index"); //47
      }

      ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
      ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
      return View(album);
    }

    //
    // GET: /StoreManager/Edit/5

    public ActionResult Edit(int id = 0) {
      Album album = db.Albums.Find(id);
      if (album == null) {
        return HttpNotFound();
      }
      ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
      ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
      return View(album);
    }

    //
    // POST: /StoreManager/Edit/5

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Album album) {
      if (ModelState.IsValid) {
        db.Entry(album).State = EntityState.Modified; // 70
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
      ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
      return View(album);
    }

    //
    // GET: /StoreManager/Delete/5

    public ActionResult Delete(int id = 0) {
      Album album = db.Albums.Find(id);
      if (album == null) {
        return HttpNotFound();
      }
      return View(album);
    }

    //
    // POST: /StoreManager/Delete/5

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id) {
      Album album = db.Albums.Find(id);
      db.Albums.Remove(album);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing) {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}